Attribute VB_Name = "isopatch"
Option Explicit

'iso_type:
Public Const ISO9660_M1 = 0               'Mode 1
Public Const ISO9660_M2F1 = 1             'Mode 2 Form 1
Public Const ISO9660_M2F2 = 2             'Mode 2 Form 2

Public Declare Function iso_patch_file Lib "isopatch.dll" (ByVal iso_type As Long, ByVal iso_file As String, ByVal need_ecc_edc As Long, ByVal iso_offset As Long, ByVal patch_list_file As String) As Long
Public Declare Function iso_patch_byte Lib "isopatch.dll" (ByVal iso_type As Long, ByVal iso_file As String, ByVal need_ecc_edc As Long, ByVal iso_offset As Long, ByRef patch_buf() As Byte, ByVal patch_bytes As Long) As Long
Public Declare Function iso_patch_list Lib "isopatch.dll" (ByVal iso_type As Long, ByVal iso_file As String, ByVal need_ecc_edc As Long, ByVal patch_list_file As String) As Long

