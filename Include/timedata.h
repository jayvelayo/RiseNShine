/**
 * timedata.h
 *
 */

#ifndef timedata_H_
#define timedata_H_

class timedata {
    unsigned int hour;
    unsigned int minute;
    unsigned int second;

public:

    timedata() : hour(0), minute(0), second(0) {}
    timedata(unsigned int hr,unsigned int min, unsigned int sec) : hour(hr), minute(min), second(sec) {}

    friend bool operator==(const timedata& A, const timedata& B) {
        return ((A.hour == B.hour) && (A.minute == B.minute) && (A.second == B.second));
    }

    inline void incrementSecond() {
        second++;
        if (second == 60) {
            second = 0;
            minute++;
        }

        if (minute == 60) {
            minute = 0;
            hour++;
        }

        if (hour==24) hour = 0;
    }

    void updateValues(unsigned int data0, unsigned int data1, unsigned int data2) {
        hour = data0;
        minute = data1;
        second = data2;
    }

    void updateValues(int (&data)[3]) {
        hour = data[0];
        minute = data[1];
        second = data[2];
    }
};

#endif
