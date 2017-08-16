#define WIN32_LEAN_AND_MEAN

#include <windows.h>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdlib.h>
#include <stdio.h>


// Need to link with Ws2_32.lib, Mswsock.lib, and Advapi32.lib
#pragma comment (lib, "Ws2_32.lib")
#pragma comment (lib, "Mswsock.lib")
#pragma comment (lib, "AdvApi32.lib")


#define DEFAULT_BUFLEN 512

WSADATA wsaData;
SOCKET ConnectSocket = INVALID_SOCKET;
struct addrinfo *result = NULL,
	*ptr = NULL,
	hints;
char recvbuf[DEFAULT_BUFLEN];
int iResult;
int recvbuflen = DEFAULT_BUFLEN;

int __cdecl main(int argc, char **argv)
{
	if (Connect("10.229.18.134", "15556") == 0) {
		char * logMsg = "<?xml version=\"1.0\" encoding=\"utf - 8\"?><logdata><devicename>iphone</devicename><log>this is a test log</log><date>8/7/2017 12:37:58 PM</date><type>0</type></logdata><data_split>";
		SendLog(logMsg);
		SendLog(logMsg);
		SendLog(logMsg);
	}
	system("pause");
	return 0;
}

int __cdecl Connect(char *ip, char *port)
{
	printf("usage: %s server-name\n", ip);

	// Initialize Winsock
	iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
	if (iResult != 0) {
		printf("WSAStartup failed with error: %d\n", iResult);
		return 1;
	}

	ZeroMemory(&hints, sizeof(hints));
	hints.ai_family = AF_UNSPEC;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;

	// Resolve the server address and port
	iResult = getaddrinfo(ip, port, &hints, &result);
	if (iResult != 0) {
		printf("getaddrinfo failed with error: %d\n", iResult);
		WSACleanup();
		return 1;
	}

	// Attempt to connect to an address until one succeeds
	for (ptr = result; ptr != NULL; ptr = ptr->ai_next) {

		// Create a SOCKET for connecting to server
		ConnectSocket = socket(ptr->ai_family, ptr->ai_socktype,
			ptr->ai_protocol);
		if (ConnectSocket == INVALID_SOCKET) {
			printf("socket failed with error: %ld\n", WSAGetLastError());
			WSACleanup();
			return 1;
		}

		// Connect to server.
		iResult = connect(ConnectSocket, ptr->ai_addr, (int)ptr->ai_addrlen);
		if (iResult == SOCKET_ERROR) {
			closesocket(ConnectSocket);
			ConnectSocket = INVALID_SOCKET;
			continue;
		}
		break;
	}

	freeaddrinfo(result);

	if (ConnectSocket == INVALID_SOCKET) {
		printf("Unable to connect to server!\n");
		WSACleanup();
		return 1;
	}
	return 0;// connect succedd;
}
int __cdecl SendLog(char *log)
{
	// Send an initial buffer
	iResult = send(ConnectSocket, log, (int)strlen(log), 0);
	if (iResult == SOCKET_ERROR) {
		printf("send failed with error: %d\n", WSAGetLastError());
		closesocket(ConnectSocket);
		WSACleanup();
		return 1;
	}
	return 0;
}
int __cdecl DisConnect()
{
	//send a signal to server
	SendLog("[&client_disconnected]");
	// shutdown the connection since no more data will be sent
	iResult = shutdown(ConnectSocket, SD_SEND);
	if (iResult == SOCKET_ERROR) {
		printf("shutdown failed with error: %d\n", WSAGetLastError());
		closesocket(ConnectSocket);
		WSACleanup();
		return 1;
	}
	// cleanup
	closesocket(ConnectSocket);
	WSACleanup();
	return 0;
}
void _cdecl MsgMonitor()
{
	// Receive until the peer closes the connection
	do {
		iResult = recv(ConnectSocket, recvbuf, recvbuflen, 0);
		if (iResult > 0)
			printf("Bytes received: %d\n", iResult);
		else if (iResult == 0)
			printf("Connection closed\n");
		else
			printf("recv failed with error: %d\n", WSAGetLastError());

	} while (iResult > 0);
}


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

