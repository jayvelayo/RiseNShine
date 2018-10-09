/**
 * messageAPI.cpp
 *
 */


#include "messageAPI.h"

void MessageAPI::conv16bit_to_8bits(const unsigned int& data, char* array, const unsigned int& i) {
	array[i] = (data >> 8) & 0xff;
	array[i+1] = data & 0xff;
}

int MessageAPI::conv8bits_to_16bit(const char* array, const unsigned int& index) {
	int data = ((array[index] & 0xff) << 8) + (array[index+1] & 0xff);
	return data;
}
