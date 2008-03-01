#include "stdafx.h"
#include "memmem.h"

#define MAX(x,y) ((x)>(y))?(x):(y)

unsigned char* memmem(
	unsigned char* haystack, size_t hlen,
	unsigned char* needle,   size_t nlen)
{
	return boyermoore_horspool_memmem(haystack, hlen, needle, nlen);
}

size_t bad_char_skip[UCHAR_MAX + 1]; /* Officially called:
                                * bad character shift */
// 16 seconds
unsigned char* boyermoore_horspool_memmem(
	unsigned char* haystack, size_t hlen,
	unsigned char* needle,   size_t nlen)
{
    size_t scan = 0;
 
    /* Sanity checks on the parameters */
    if (nlen <= 0 || !haystack || !needle)
        return NULL;
 
    /* ---- Preprocess ---- */
    /* Initialize the table to default value */
    /* When a character is encountered that does not occur
     * in the needle, we can safely skip ahead for the whole
     * length of the needle.
     */
    for (scan = 0; scan <= UCHAR_MAX; scan++) bad_char_skip[scan] = nlen;
 
    size_t last = nlen - 1;
 
    /* Then populate it with the analysis of the needle */
    for (scan = 0; scan < last; scan++) bad_char_skip[needle[scan]] = last - scan;
 
    /* Search the haystack, while the needle can still be within it. */
    while (hlen >= nlen)
    {
        /* scan from the end of the needle */
        for (scan = last; haystack[scan] == needle[scan]; scan--)
		{
            if (scan == 0) return haystack;
		}
 
        hlen     -= bad_char_skip[haystack[last]];
        haystack += bad_char_skip[haystack[last]];
    }
 
    return NULL;
}

