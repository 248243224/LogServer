#include <stdio.h>      /* for printf() and fprintf() */

#include <sys/socket.h> /* for socket(), connect(), send(), and recv() */
#include <arpa/inet.h>  /* for sockaddr_in and inet_addr() */
#include <stdlib.h>     /* for atoi() and exit() */
#include <string.h>     /* for memset() */
#include <unistd.h>     /* for close() */
#include <netinet/tcp.h>

#define USBSERVER  "127.0.0.1"
#define USBPORT  "15555"

int sock;                        /* Socket descriptor */
struct sockaddr_in echoServAddr; /* Echo server address */
unsigned short echoServPort;     /* Echo server port */
char *servIP;                    /* Server IP address (dotted quad) */


int UsbConnect(){
    return Connect(USBSERVER, USBPORT);
}

int Connect(char *ip, char *port)
{
    servIP = ip;              /* server IP address (dotted quad) */
    echoServPort = atoi(port);  /* server port */
    /* Create a reliable, stream socket using TCP */
    if ((sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP)) < 0)
        return 0;
    /* Construct the server address structure */
    memset(&echoServAddr, 0, sizeof(echoServAddr));     /* Zero out structure */
    echoServAddr.sin_family      = AF_INET;             /* Internet address family */
    echoServAddr.sin_addr.s_addr = inet_addr(servIP);   /* Server IP address */
    echoServAddr.sin_port        = htons(echoServPort); /* Server port */

    /* Establish the connection to the echo server */
    if (connect(sock, (struct sockaddr *) &echoServAddr, sizeof(echoServAddr)) < 0) {
        return 0;
    }
    int yes = 0;
    int result = setsockopt(sock,
                            IPPROTO_TCP,
                            TCP_NODELAY,  // to enable send no delay
                            (char *) &yes,
                            sizeof(int));    // 1 - on, 0 - off
    if (result < 0)
        return  0;
    return  1;
}
//
//type-> 0:info,1:warning,2:error
//
int SendLog(char *deviceName,char *log,char *date,char *type) {

    char *logXmlPart1 = "<?xml version=\"1.0\" encoding=\"utf - 8\"?><logdata><devicename>";
    char *logXmlPart2 = "</devicename><log>";
    char *logXmlPart3 = "</log><date>";
    char *logXmlPart4 = "</date><type>";
    char *logXmlPart5 = "</type></logdata><data_split>";
    //generate log xml
    char *logXml = malloc(strlen(logXmlPart1) + strlen(logXmlPart2)
                          + strlen(logXmlPart3) + strlen(logXmlPart4) + strlen(logXmlPart5)
                          + strlen(deviceName) + strlen(log) + strlen(date) + strlen(type) +
                          1);//+1 for the zero-terminator
    if (logXml == NULL) return 0;
    strcpy(logXml, logXmlPart1);
    strcat(logXml, deviceName);
    strcat(logXml, logXmlPart2);
    strcat(logXml, log);
    strcat(logXml, logXmlPart3);
    strcat(logXml, date);
    strcat(logXml, logXmlPart4);
    strcat(logXml, type);
    strcat(logXml, logXmlPart5);
    /* Send this */
    if (send(sock, logXml, strlen(logXml), 0) == -1)
        return 0;
    return 1;
}

void DisConnect()
{
    close(sock);
}

