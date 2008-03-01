#include <stddef.h> 
#include <string.h> 
#include <limits.h>

#undef memmem 

/* Return the first occurrence of NEEDLE in HAYSTACK. */ 
unsigned char* memmem (unsigned char* haystack, 
			  size_t haystack_len, 
			  unsigned char* needle, 
			  size_t needle_len);

unsigned char* boyermoore_horspool_memmem(
	unsigned char* haystack, size_t hlen,
	unsigned char* needle,   size_t nlen);

