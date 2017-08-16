#include <jni.h>
#include <string>




extern "C" {
int UsbConnect();
int SendLog(char *deviceName, char *log, char *date, char *type);
void DisConnect();
int Connect(char *ip, char *port);

JNIEXPORT jint JNICALL
Java_com_kodakalaris_jane_logclient_MainActivity_usbConnect(JNIEnv *env, jobject instance) {

    // TODO
    return UsbConnect();
}

JNIEXPORT jint JNICALL
Java_com_kodakalaris_jane_logclient_MainActivity_wifiConnect(JNIEnv *env, jobject instance,
                                                             jstring ip_, jstring port_) {
    const char *ip = env->GetStringUTFChars(ip_, 0);
    const char *port = env->GetStringUTFChars(port_, 0);

    // TODO
    int lengthIp = strlen(ip);
    char *_ip = new char[lengthIp + 1];
    strcpy(_ip, ip);
    int lengthPort = strlen(port);
    char *_port = new char[lengthPort + 1];
    strcpy(_port, port);
    int res = Connect(_ip, _port);

    env->ReleaseStringUTFChars(ip_, ip);
    env->ReleaseStringUTFChars(port_, port);
    return res;
}

JNIEXPORT void JNICALL
Java_com_kodakalaris_jane_logclient_MainActivity_disConnect(JNIEnv *env, jobject instance) {

    // TODO
    DisConnect();
}

JNIEXPORT jint JNICALL
Java_com_kodakalaris_jane_logclient_MainActivity_sendLog(JNIEnv *env, jobject instance,
                                                         jstring device_, jstring log_,
                                                         jstring date_, jstring type_) {
    const char *device = env->GetStringUTFChars(device_, 0);
    const char *log = env->GetStringUTFChars(log_, 0);
    const char *date = env->GetStringUTFChars(date_, 0);
    const char *type = env->GetStringUTFChars(type_, 0);

    // TODO
    int lengthDevice = strlen(device);
    char *_device = new char[lengthDevice + 1];
    strcpy(_device, device);

    int lengthLog = strlen(log);
    char *_log = new char[lengthLog + 1];
    strcpy(_log, log);

    int lengthDate = strlen(date);
    char *_date = new char[lengthDate + 1];
    strcpy(_date, date);

    int lengthType = strlen(type);
    char *_type = new char[lengthType + 1];
    strcpy(_type, type);

    int res = SendLog(_device, _log, _date, _type);

    env->ReleaseStringUTFChars(device_, device);
    env->ReleaseStringUTFChars(log_, log);
    env->ReleaseStringUTFChars(date_, date);
    env->ReleaseStringUTFChars(type_, type);

    return res;
}

}
