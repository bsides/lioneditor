#include <stdio.h>
#include <string.h>

#define MAXBUFFER 209715200 //200Mb

long int header22;
int filesize, i, start_pointer, end_pointer, size, dummy_count;
int buffer[MAXBUFFER];
char filename[80];

FILE *input, *output;

int main(int argc, char *argv[])
{
    if (argc < 3)
    {
        printf("Final Fantasy Tactics PSP De/Archivier 1.0 by Brisma\n"
               "English translation by Joe Davidson\n"
               "   -> fftpspext -d fftpack.bin //Extract\n"
               "   -> fftpspext -a fftpack.bin //Update\n"
               "   -> fftpspext -r newfile.bin //Rebuild\n"
               "   -> fftpspext -d22 fftpack.22 //Extract fftpack.22\n"
               "   -> fftpspext -r22 fftpack.22 //Rebuild fftpack.22\n"
               "   -> fftpspext -d772 fftpack.772 //Extract fftpack.772\n"
               "   -> fftpspext -r772 fftpack.772 //Rebuild fftpack.772\n");
    }
                         
    else if(strcmp(argv[1], "-d") == 0)
    { //Dearchivia
        printf("Extracting file %s...", argv[2]);
        input = fopen(argv[2], "rb");
        
        // Get filesize
        fseek(input, 0, SEEK_END);
        filesize = ftell(input);
        printf("Original filesize: %d\n", filesize);
        
        fseek(input, 0x08, SEEK_SET);
        
        // Where does 3970 come from?
        // It is the number of entries in the file table
        for(i=1; i <= 3970; i++)
        {
            // Read an int
            fread(&start_pointer, 4, 1, input);
            
            if (i == 3970) 
            {
                end_pointer = filesize;
            }
            else 
            {
                fread(&end_pointer, 4, 1, input);
            }
            
            size = end_pointer - start_pointer;
            
            if (size != 0) 
            {
                printf("\nExtracting file fftpack.%d (%d bytes)", i, size);
                sprintf(filename, "fftpack.%d", i);
                output=fopen(filename, "wb");
                fseek(input, start_pointer, SEEK_SET);
                fread(&buffer, size, 1, input);
                fwrite(&buffer, size, 1, output);
                fclose(output);
                printf(" [OK]");
            }
            else 
            {
                printf("\nIgnoring dummy file fftpack.%d [IGNORED]", i); //Dummy
                dummy_count++;
            }
            
            // Return to file table and get next file
            fseek(input, (4 * i) + 8, SEEK_SET);
        }
    
        fclose(input);
        
        printf("\n\nExtracted %d files, ignored %d dummy files.", (3970-dummy_count), dummy_count);
    }
    
    else if(strcmp(argv[1], "-r") == 0)
    { //Riarchivia
        printf("Rebuilding file %s...", argv[2]);
        output=fopen(argv[2], "wb");
        
        //Rebuild header manually 
        fputc(0x80, output);
        fputc(0x0F, output);
        fputc(0x00, output);
        fputc(0x00, output);
        fputc(0x10, output);
        fputc(0x00, output);
        fputc(0x00, output);
        fputc(0x00, output);
        
        fseek(output, 15888, SEEK_SET);
        end_pointer=ftell(output);
        
        // end_pointer is where we'll start writing data
        
        start_pointer=8;
        for(i=1; i <= 3970; i++)
        {
            printf("\nReinserting file fftpack.%d at offset 0x%X...", i, end_pointer);

            // Write the address of the beginning of the file to the file table
            fseek(output, start_pointer, SEEK_SET);
            fwrite(&end_pointer, 4, 1, output);
            start_pointer+=4;
            
            fseek(output, end_pointer, SEEK_SET);
            sprintf(filename, "fftpack.%d", i);
            if ((input = fopen(filename, "rb")) == NULL) 
            { 
                printf(" [IGNORED]"); 
                dummy_count++; 
            }
            else 
            {
                fseek(input, 0, SEEK_END);
                size=ftell(input);
                fseek(input, 0, SEEK_SET);
                fread(&buffer, size, 1, input);
                fclose(input);
                fwrite(&buffer, size, 1, output);
                printf(" [OK]");
            }
            end_pointer=ftell(output);
        }
        fclose(output);
        printf("\n\nRebuilt from %d files, ignoring %d dummy files", (3970-dummy_count), dummy_count);
    }
    
    else if(strcmp(argv[1], "-a") == 0)
    { 
        printf("Updating file %s...", argv[2]);
        output=fopen(argv[2], "r+b");
        for(i=1; i <= 3970; i++)
        {
            sprintf(filename, "fftpack.%d", i);
            if (!((input = fopen(filename, "rb")) == NULL))
            {
                fseek(input, 0, SEEK_END);
                filesize = ftell(input);
                fseek(input, 0, SEEK_SET);
                fseek(output, (i * 4) + 4, SEEK_SET);
                fread(&start_pointer, 4, 1, output);
                fread(&end_pointer, 4, 1, output);
                printf("\nInserting file fftpack.%d at address 0x%X...", i, start_pointer);
                if (filesize == (end_pointer - start_pointer))
                {
                    fread(&buffer, filesize, 1, input);
                    fclose(input);
                    fseek(output, start_pointer, SEEK_SET);
                    fwrite(&buffer, filesize, 1, output);
                    printf(" [OK]");
                    dummy_count++;             
                }  
                else 
                {
                    printf(" [ERROR]");
                }
            }
        }
        fclose(output);
        printf("\n\nUpdated %d file.", dummy_count);
    }
    
    else if(strcmp(argv[1], "-d22") == 0)
    { //Dearchivia fftpack.22
        printf("Opening File22 %s...", argv[2]);
        input=fopen(argv[2], "rb");
        for(i = 0; i < 7618560; i+=10240)
        {
            printf("\nFile %d...", (i / 10240) + 1);
            fseek(input, i, SEEK_SET); 
            fread(&header22, 4, 1, input);
            if (header22 == 0xF2F2F2F2) 
            { 
                dummy_count++; 
                printf(" [Does not contain text]"); 
                sprintf(filename, "%s.%d.dummy", argv[2], (i / 10240) + 1);
            }
            else 
            {
                printf(" [Contains text]");
                sprintf(filename, "%s.%d", argv[2], (i / 10240) + 1);
            }
            fseek(input, i, SEEK_SET);
            fread(&buffer, 10240, 1, input);
            output=fopen(filename, "wb");
            fwrite(&buffer, 10240, 1, output);
            fclose(output);
        }
        printf("\n\nFiles containing text: %d", 744 - dummy_count);
    }
    
    else if(strcmp(argv[1], "-r22") == 0)
    { //Riarchivia fftpack.22
        printf("Rebuilding File22 %s...", argv[2]);
        output=fopen(argv[2], "wb");
        for(i = 0; i < 7618560; i+=10240)
        {
            sprintf(filename, "%s.%d", argv[2], (i / 10240) + 1);
            if ((input=fopen(filename, "rb")) == NULL)
            {
                sprintf(filename, "%s.%d.dummy", argv[2], (i/10240)+1);
                input = fopen(filename, "rb");
            }

            printf("\nInserting file %s...", filename);
            fread(&buffer, 10240, 1, input);
            fwrite(&buffer, 10240, 1, output);
            fclose(input);
            printf(" [OK]");
        }
        fclose(output);
        printf("\n\nDone.");
    }
    
    else if(strcmp(argv[1], "-d772") == 0)
    { //Dearchivia fftpack.772
        printf("Opening File772 %s...", argv[2]);
        input=fopen(argv[2], "rb");
        for(i = 0; i < 3964928; i+=32768)
        {
            printf("\nExtracting file %d...", (i / 32768) + 1);
            fseek(input, i, SEEK_SET);
            fread(&buffer, 32768, 1, input);
            sprintf(filename, "%s.%d", argv[2], (i / 32768) + 1);
            output=fopen(filename, "wb");
            fwrite(&buffer, 32768, 1, output);
            printf(" [OK]");
            fclose(output);
        }
        printf("\n\nExtracted %d files.", (i / 32768) + 1);
    }
    
    else if(strcmp(argv[1], "-r772") == 0)
    { //Riarchivia fftpack.772
        printf("Rebuilding File772 %s...", argv[2]);
        output=fopen(argv[2], "wb");
        for(i = 0; i < 3964928; i+=32768)
        {
            sprintf(filename, "%s.%d", argv[2], (i / 32768) + 1);
            printf("\nInserting file %s...", filename);
            input=fopen(filename, "rb");
            fread(&buffer, 32768, 1, input);
            fwrite(&buffer, 32768, 1, output);
            fclose(input);
            printf(" [OK]");
        }
        fclose(output);
        printf("\n\nDone.");
    }
    
    else 
    {
        printf("Final Fantasy Tactics PSP De/Archivier 1.0 by Brisma\n"
               "   -> fftpspext -d fftpack.bin //Extract\n"
               "   -> fftpspext -a fftpack.bin //Update\n"
               "   -> fftpspext -r newfile.bin //Rebuild\n"
               "   -> fftpspext -d22 fftpack.22 //Extract fftpack.22\n"
               "   -> fftpspext -r22 fftpack.22 //Rebuild fftpack.22\n"
               "   -> fftpspext -d772 fftpack.772 //Extract fftpack.772\n"
               "   -> fftpspext -r772 fftpack.772 //Rebuild fftpack.772\n");
    }
    
    return 0;
}
