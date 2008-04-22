#include <stdio.h>
#include <string.h>

/*
JP
0xC0A2: 40DB3700 0037DB40 -> E4D93700 0037D9E4

Enc: 0x010000 - 0x38DB3F (0x37DB40 bytes)
Dec: 0x0FA68000 - 0x0FDE59E3 (0x37D9E4 bytes)

EU

Enc: 0x010000 - 0x3B85FF (0x3A8600 bytes)
Dec: 0x0FED8000 - 0x102804A3 (0x3A84A4 bytes)

US

0xC0A2: 00863A00 003A8600 -> A4843A00 003A84A4
Enc: 0x010000 - 0x3B85FF (0x3A8600 bytes)
Dec: 0x0FED8000 - 0x102804A3 (0x3A84A4 bytes)
*/

#define BUFFER_SIZE 1024
#define TRUE 1
#define FALSE 0

int IsJP(FILE* fp);
int IsEU(FILE* fp);
int IsUS(FILE* fp);
int CopyBytes(FILE* fp, unsigned int src, unsigned int srcSize, unsigned int dest, unsigned int destOldSize);

unsigned char buffer[BUFFER_SIZE] = { '\0' };
unsigned char jpSizes[8] = { 0xE4, 0xD9, 0x37, 0x00, 0x00, 0x37, 0xD9, 0xE4 };
unsigned char euSizes[8] = { 0xA4, 0x84, 0x3A, 0x00, 0x00, 0x3A, 0x84, 0xA4 };

int main(int argc, char** argv)
{
    if (argc != 2)
    {
        printf("Usage: %s filename.iso\n", argv[0]);
        return -1;
    }
    
    FILE* fp = fopen(argv[1], "rb+");
    if (fp == NULL)
    {
        printf("Could not open file %s\n", argv[1]);
        return -2;
    }
    
    if (IsJP(fp))
    {
        CopyBytes(fp, 0x0FA68000, 0x37D9E4, 0x010000, 0x37DB40);
        if ((fseek(fp, 0xC0A2, SEEK_SET) != 0) || (fwrite(jpSizes, 1, 8, fp) != 8))
        {
            printf("Something terrible happened\n");
            fclose(fp);
            fp = NULL;
            return -4;
        }
    }
    else if (IsUS(fp) || IsEU(fp))
    {
        CopyBytes(fp, 0x0FED8000, 0x3A84A4, 0x010000, 0x3A8600);
        fseek(fp, 0xC0A2, SEEK_SET);
        if ((fseek(fp, 0xC0A2, SEEK_SET) != 0) || (fwrite(euSizes, 1, 8, fp) != 8))
        {
            printf("Something terrible happened\n");
            fclose(fp);
            fp = NULL;
            return -4;
        }
    }
    else
    {
        printf("%s is not a known War of the Lions image file\n", argv[1]);
        fclose(fp);
        fp = NULL;
        return -3;
    }
    
    fclose(fp);
    fp = NULL;
    return 0;
}

int CopyBytes(FILE* fp, unsigned int src, unsigned int srcSize, unsigned int dest, unsigned int destOldSize)
{
    unsigned int bytesRead = 0;
    
    while ((bytesRead + BUFFER_SIZE) < srcSize)
    {
        if (fseek(fp, src + bytesRead, SEEK_SET) != 0) return -1;
        if (fread(buffer, 1, 1024, fp) != 1024) return -1;
        if (fseek(fp, dest + bytesRead, SEEK_SET) != 0) return -1;
        if (fwrite(buffer, 1, 1024, fp) != 1024) return -1;
        bytesRead += BUFFER_SIZE;
    }
    
    if (fseek(fp, src + bytesRead, SEEK_SET) != 0) return -1;
    if (fread(buffer, 1, srcSize-bytesRead, fp) != (srcSize-bytesRead)) return -1;
    if (fseek(fp, dest + bytesRead, SEEK_SET) != 0) return -1;
    if (fwrite(buffer, 1, srcSize-bytesRead, fp) != (srcSize-bytesRead)) return -1;
    bytesRead = srcSize;
    
    if (destOldSize > srcSize)
    {
        memset(buffer, 0, destOldSize-srcSize);
        if (fseek(fp, dest + srcSize, SEEK_SET) != 0) return -1;
        if (fwrite(buffer, 1, destOldSize-srcSize, fp) != (destOldSize-srcSize)) return -1;
   }
   
   return 0;
}

int CheckFile(FILE* fp, char* str1, char* str2, long int loc1, long int loc2, long int loc3, long int loc4, long int loc5)
{
    if (fseek(fp, loc1, SEEK_SET) != 0) return FALSE;
    if (fread(buffer, 1, 10, fp) != 10) return FALSE;
    buffer[10] = '\0';
    if (strcmp(str1, (char*)buffer) != 0) return FALSE;

    if (fseek(fp, loc2, SEEK_SET) != 0) return FALSE;
    if (fread(buffer, 1, 10, fp) != 10) return FALSE;
    buffer[10] = '\0';
    if (strcmp(str1, (char*)buffer) != 0) return FALSE;

    if (fseek(fp, loc3, SEEK_SET) != 0) return FALSE;
    if (fread(buffer, 1, 9, fp) != 9) return FALSE;
    buffer[9] = '\0';
    if (strcmp(str2, (char*)buffer) != 0) return FALSE;

    if (fseek(fp, loc4, SEEK_SET) != 0) return FALSE;
    if (fread(buffer, 1, 9, fp) != 9) return FALSE;
    buffer[9] = '\0';
    if (strcmp(str2, (char*)buffer) != 0) return FALSE;

    if (fseek(fp, loc5, SEEK_SET) != 0) return FALSE;
    if (fread(buffer, 1, 9, fp) != 9) return FALSE;
    buffer[9] = '\0';
    if (strcmp(str2, (char*)buffer) != 0) return FALSE;

    return TRUE;
}

int IsJP(FILE* fp)
{
    //0x8373 "ULJM-05194"
    //0xe000 "ULJM-05194"
    //0x2BF0128 "ULJM05194"
    //0xfd619fc "ULJM05194"
    //0xfd97a5c "ULJM05194"
    return CheckFile(fp, "ULJM-05194", "ULJM05194", 0x8373, 0xE000, 0x2BF0128, 0xFD619FC, 0xFD97A5C);
}

int IsEU(FILE* fp)
{
    //0x8373 "ULES-00850"
    //0xe000 "ULES-00850"
    //0x2c18128 "ULES00850"
    //0x101ec3a8 "ULES00850"
    //0x10232530 "ULES00850"
    return CheckFile(fp, "ULES-00850", "ULES00850", 0x8373, 0xE000, 0x2C18128, 0x101EC3A8, 0x10232530);
}

int IsUS(FILE* fp)
{
    //0x8373 "ULUS-10297"
    //0xe000 "ULUS-10297"
    //0x2c18128 "ULUS10297"
    //0x101ec3a8 "ULUS10297"
    //0x10232530 "ULUS10297"
    return CheckFile(fp, "ULUS-10297", "ULUS10297", 0x8373, 0xE000, 0x2C18128, 0x101EC3A8, 0x10232530);
}
