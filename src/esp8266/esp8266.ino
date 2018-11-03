#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <ArduinoJson.h>
#include "ESP8266MessageInterface.h"
 
const char* ssid = "TP-LINK_5FA9";
const char* password = "janissa01";

const char* SSHTMLSOURCE = "http://api.sunrise-sunset.org/json?lat=49.2827&lng=-123.1207&formatted=0";
const char* CURRTIMESOURCE = "http://worldclockapi.com/api/json/utc/now";

typedef struct _TIME {
  unsigned int year;
  unsigned int month;
  unsigned int day;
  unsigned int hour;
  unsigned int minute;
  unsigned int second;
} tme;

typedef struct _TIME_DATA {
  tme sunset;
  tme sunrise;
  tme currTime;
  bool valid;
} timeData_t;

enum DataContent {
  SSdata,
  currTimeData
};

ESP8266MsgAPI api;
timeData_t timeData;

void getHTMLcontents(timeData_t& data, enum DataContent dContent);
void parseSSJson(timeData_t& data, String& str);
void parseCurrTimeJson(timeData_t& data, String& str);
void listenForCommand();
void sendDataToMSP(cmdType cmd, int* data);
 
void setup () {
 
  Serial.begin(9600);
  WiFi.begin(ssid, password);
 
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
  }
}
 
void loop() {
  getHTMLcontents(timeData, SSdata);
  getHTMLcontents(timeData, currTimeData);
  listenForCommand();
}

void listenForCommand() {
  cmdType cmd;
  int data[3];

  //do nothing until a packet is ready to be read
  if (Serial.available() > 8) {
    api.recvPacket(cmd, data);
    sendDataToMSP(cmd, data);
  }
}

void sendDataToMSP(cmdType cmd, int* data) {
  switch(cmd) {
    case getSunsetTime:
      getHTMLcontents(timeData, SSdata);
      api.sendPacket(cmd, timeData.sunset.hour, timeData.sunset.minute, timeData.sunset.second);
      break;
    case getSunriseTime:
      getHTMLcontents(timeData, SSdata);
      api.sendPacket(cmd, timeData.sunrise.hour, timeData.sunrise.minute, timeData.sunrise.second);
      break;
    case getCurrentTime:
      getHTMLcontents(timeData, currTimeData);
      api.sendPacket(cmd, timeData.currTime.hour, timeData.currTime.minute, timeData.currTime.second);
      break;
    case 43: //echo test if received and transmitted packet is the same
      api.sendPacket(cmd, data[0], data[1], data[2]);
      break;  
    default:
      Serial.println("invalid cmd");
      break;
  }
}

void getHTMLcontents(timeData_t& data, enum DataContent dContent) {
  String payload = "";
  
  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http; 

   switch(dContent) {
    case SSdata: 
      http.begin(SSHTMLSOURCE);  //Specify request destination
      break;
    case currTimeData :
      http.begin(CURRTIMESOURCE);  //Specify request destination
      break;
   }
    
    int httpCode = http.GET();                                                                      
    if (httpCode > 0) { //Check the returning code 
      payload += http.getString();   //Get the request response payload
    }  
    http.end();   //Close connection
  }

  
  switch(dContent) {
    case SSdata: 
      parseSSJson(data, payload);
      break;
    case currTimeData :
      parseCurrTimeJson(data,payload);
      break;
   }
}

void parseSSJson(timeData_t& data, String& str) {
  //convert JSON payload to usable data
  StaticJsonBuffer<816> jsonBuffer;
  JsonObject& root = jsonBuffer.parseObject(str);

  //check if it succeed
  if (!root.success() ) {
    data.valid = false;
    //send error code
    return;
  }
  String sunrise_data = root["results"]["sunrise"];
  String sunset_data = root["results"]["sunset"];
  int tzh, tzm;
  data.valid = true;
  sscanf(sunset_data.c_str(), "%d-%d-%dT%d:%d:%d+%d:%d", &(data.sunset.year), &(data.sunset.month), &(data.sunset.day), &(data.sunset.hour), &(data.sunset.minute), &(data.sunset.second), &tzh, &tzh);
  sscanf(sunrise_data.c_str(), "%d-%d-%dT%d:%d:%d+%d:%d", &(data.sunrise.year), &(data.sunrise.month), &(data.sunrise.day), &(data.sunrise.hour), &(data.sunrise.minute), &(data.sunrise.second), &tzh, &tzh);
}

void parseCurrTimeJson(timeData_t& data, String& str) {
  //convert JSON payload to usable data
  StaticJsonBuffer<558> jsonBuffer;
  JsonObject& root = jsonBuffer.parseObject(str);

  //check if it succeed
  if (!root.success() ) {
    //send error code
    return;
  }
  String sunrise_data = root["currentDateTime"];
  sscanf(sunrise_data.c_str(), "%d-%d-%dT%d:%dZ", &(data.currTime.year), &(data.currTime.month), &(data.currTime.day), &(data.currTime.hour), &(data.currTime.minute));
  data.currTime.second = 0;
}

