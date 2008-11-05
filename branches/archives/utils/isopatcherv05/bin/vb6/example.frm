VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "ISO Patcher Example"
   ClientHeight    =   3195
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4680
   LinkTopic       =   "Form1"
   ScaleHeight     =   3195
   ScaleWidth      =   4680
   StartUpPosition =   3  'Windows Default
   Begin VB.TextBox txtISO 
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   390
      TabIndex        =   2
      Text            =   "g:\wl.bin"
      Top             =   405
      Width           =   3990
   End
   Begin VB.CheckBox chkECCEDC 
      Caption         =   "ECC/EDC calculate"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   405
      TabIndex        =   1
      Top             =   1890
      Value           =   1  'Checked
      Width           =   2280
   End
   Begin VB.CommandButton Command1 
      Caption         =   "patch"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   2850
      TabIndex        =   0
      Top             =   2595
      Width           =   1560
   End
   Begin VB.Frame Frame1 
      Caption         =   "ISO File"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   765
      Left            =   210
      TabIndex        =   3
      Top             =   135
      Width           =   4320
   End
   Begin VB.TextBox txtLst 
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   360
      Left            =   405
      TabIndex        =   4
      Text            =   "g:\1.lst"
      Top             =   1260
      Width           =   4005
   End
   Begin VB.Frame Frame2 
      Caption         =   "Patch List File"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   9
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   765
      Left            =   225
      TabIndex        =   5
      Top             =   990
      Width           =   4320
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Private Sub Command1_Click()

    Dim nTotalPatched As Long
    Dim nNeedECCEDC As Long
    
    If chkECCEDC.Value = True Then
        nNeedECCEDC = 1
    Else
        nNeedECCEDC = 0
    End If

    nTotalPatched = iso_patch_list(ISO9660_M1, txtISO.Text, nNeedECCEDC, txtLst.Text)
    
    If nTotalPatched >= 0 Then
        MsgBox "total patched bytes: " & nTotalPatched
    Else
        MsgBox "error code: " & nTotalPatched
    End If
    
End Sub

