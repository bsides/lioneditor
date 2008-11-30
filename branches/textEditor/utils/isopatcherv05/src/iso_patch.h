#ifndef _ISO_PATCH_H
#define _ISO_PATCH_H

int WINAPI iso_patch_file(int iso_type, char *iso_file, int need_ecc_edc, int iso_offset, char *patch_file);
int WINAPI iso_patch_byte(int iso_type, char *iso_file, int need_ecc_edc, int iso_offset, char *patch_buf, int patch_bytes);
int WINAPI iso_patch_list(int iso_type, char *iso_file, int need_ecc_edc, char *patch_list_file);

#endif