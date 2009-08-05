﻿
<System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)> _
Public Structure SCARD_IO_REQUEST

    '''DWORD->unsigned int
    Public dwProtocol As UInteger

    '''DWORD->unsigned int
    Public cbPciLength As UInteger
End Structure


Public Class Form1

    '''SCARD_SCOPE_TERMINAL -> 1
    Public Const SCARD_SCOPE_TERMINAL As Integer = 1

    '''SCARD_SCOPE_SYSTEM -> 2
    Public Const SCARD_SCOPE_SYSTEM As Integer = 2

    '''SCARD_SCOPE_USER -> 0
    Public Const SCARD_SCOPE_USER As Integer = 0

    '''SCARD_SHARE_EXCLUSIVE -> 1
    Public Const SCARD_SHARE_EXCLUSIVE As Integer = 1

    '''SCARD_SHARE_SHARED -> 2
    Public Const SCARD_SHARE_SHARED As Integer = 2

    '''SCARD_SHARE_DIRECT -> 3
    Public Const SCARD_SHARE_DIRECT As Integer = 3

    '''SCARD_PROTOCOL_UNDEFINED -> 0x00000000
    Public Const SCARD_PROTOCOL_UNDEFINED As Integer = 0

    '''SCARD_PROTOCOL_OPTIMAL -> 0x00000000
    Public Const SCARD_PROTOCOL_OPTIMAL As Integer = 0

    '''SCARD_PROTOCOL_DEFAULT -> 0x80000000
    Public Const SCARD_PROTOCOL_DEFAULT As Integer = -2147483648

    '''SCARD_PROTOCOL_RAW -> 0x00010000
    Public Const SCARD_PROTOCOL_RAW As Integer = 65536

    '''SCARD_PROTOCOL_Tx -> (SCARD_PROTOCOL_T0 | SCARD_PROTOCOL_T1)
    Public Const SCARD_PROTOCOL_Tx As Integer = (SCARD_PROTOCOL_T0 Or SCARD_PROTOCOL_T1)

    '''SCARD_PROTOCOL_T1 -> 0x00000002
    Public Const SCARD_PROTOCOL_T1 As Integer = 2

    '''SCARD_PROTOCOL_T0 -> 0x00000001
    Public Const SCARD_PROTOCOL_T0 As Integer = 1

    '''Return Type: LONG->int
    '''dwScope: DWORD->unsigned int
    '''pvReserved1: LPCVOID->void*
    '''pvReserved2: LPCVOID->void*
    '''phContext: LPSCARDCONTEXT->SCARDCONTEXT*
    <System.Runtime.InteropServices.DllImportAttribute("winscard.dll", EntryPoint:="SCardEstablishContext")> _
    Public Shared Function SCardEstablishContext(ByVal dwScope As UInteger, ByVal pvReserved1 As System.IntPtr, ByVal pvReserved2 As System.IntPtr, ByRef phContext As UInteger) As Integer
    End Function
    '''Return Type: LONG->int
    '''hContext: SCARDCONTEXT->ULONG_PTR->unsigned int
    <System.Runtime.InteropServices.DllImportAttribute("winscard.dll", EntryPoint:="SCardReleaseContext")> _
    Public Shared Function SCardReleaseContext(ByVal hContext As UInteger) As Integer
    End Function
    '''Return Type: LONG->int
    '''hContext: SCARDCONTEXT->ULONG_PTR->unsigned int
    '''mszGroups: LPCWSTR->WCHAR*
    '''mszReaders: LPWSTR->WCHAR*
    '''pcchReaders: LPDWORD->DWORD*
    <System.Runtime.InteropServices.DllImportAttribute("winscard.dll", EntryPoint:="SCardListReadersW")> _
    Public Shared Function SCardListReadersW(ByVal hContext As UInteger, <System.Runtime.InteropServices.InAttribute(), System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)> ByVal mszGroups As String, <System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)> ByVal mszReaders As System.Text.StringBuilder, ByRef pcchReaders As UInteger) As Integer
    End Function

    '''Return Type: LONG->int
    '''hContext: SCARDCONTEXT->ULONG_PTR->unsigned int
    '''szReader: LPCWSTR->WCHAR*
    '''dwShareMode: DWORD->unsigned int
    '''dwPreferredProtocols: DWORD->unsigned int
    '''phCard: LPSCARDHANDLE->SCARDHANDLE*
    '''pdwActiveProtocol: LPDWORD->DWORD*
    <System.Runtime.InteropServices.DllImportAttribute("winscard.dll", EntryPoint:="SCardConnectW")> _
    Public Shared Function SCardConnectW(ByVal hContext As UInteger, <System.Runtime.InteropServices.InAttribute(), System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)> ByVal szReader As String, ByVal dwShareMode As UInteger, ByVal dwPreferredProtocols As UInteger, ByRef phCard As UInteger, ByRef pdwActiveProtocol As UInteger) As Integer
    End Function
    '''Return Type: LONG->int
    '''hCard: SCARDHANDLE->ULONG_PTR->unsigned int
    '''pioSendPci: LPCSCARD_IO_REQUEST->SCARD_IO_REQUEST*
    '''pbSendBuffer: LPCBYTE->BYTE*
    '''cbSendLength: DWORD->unsigned int
    '''pioRecvPci: LPSCARD_IO_REQUEST->_SCARD_IO_REQUEST*
    '''pbRecvBuffer: LPBYTE->BYTE*
    '''pcbRecvLength: LPDWORD->DWORD*
    <System.Runtime.InteropServices.DllImportAttribute("winscard.dll", EntryPoint:="SCardTransmit")> _
    Public Shared Function SCardTransmit(ByVal hCard As UInteger, ByRef pioSendPci As SCARD_IO_REQUEST, ByRef pbSendBuffer As Byte, ByVal cbSendLength As UInteger, ByRef pioRecvPci As SCARD_IO_REQUEST, ByRef pbRecvBuffer As Byte, ByRef pcbRecvLength As UInteger) As Integer
    End Function



    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ListBox1.Items.Clear()
    End Sub

    Private hCard As UInteger
    Private ioSendPci As SCARD_IO_REQUEST
    Private ioRecvPci As SCARD_IO_REQUEST

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim hContext As UInteger
        Dim result As Integer
        Dim sb As System.Text.StringBuilder
        Dim cchReaders As UInteger
        Dim szReader As String
        Dim protocol As UInteger

        sb = New System.Text.StringBuilder()
        result = SCardEstablishContext(SCARD_SCOPE_USER, IntPtr.Zero, IntPtr.Zero, hContext)
        ListBox1.Items.Add("SCardEstablishContext(): " + CStr(result))
        cchReaders = 256
        SCardListReadersW(hContext, Nothing, sb, cchReaders)


        ListBox1.Items.Add(sb.ToString())
        szReader = sb.ToString()

        result = SCardConnectW(hContext, szReader, SCARD_SHARE_SHARED, SCARD_PROTOCOL_T0, hCard, protocol)
        ListBox1.Items.Add("SCardConnectW(): " + CStr(result))

        ioSendPci.cbPciLength = 8
        ioSendPci.dwProtocol = protocol
        ioRecvPci.cbPciLength = 8
        ioRecvPci.dwProtocol = protocol

        Dim CmdSelectAppJPN As Byte() = {&H0, &HA4, &H4, &H0, &HA, &HA0, &H0, &H0, &H0, &H74, &H4A, &H50, &H4E, &H0, &H10}
        Dim receiveBuffer(262) As Byte
        Dim receiveBufferLength As UInteger
        receiveBufferLength = 2
        result = SCardTransmit(hCard, ioSendPci, CmdSelectAppJPN(0), CUInt(CmdSelectAppJPN.Length), ioRecvPci, receiveBuffer(0), receiveBufferLength)
        ListBox1.Items.Add("SCardTransmit() Select App : " + CStr(result) + ": Length " + CStr(receiveBufferLength))

        Dim CmdAppResponse As Byte() = {&H0, &HC0, &H0, &H0, &H5}
        receiveBufferLength = 7
        result = SCardTransmit(hCard, ioSendPci, CmdAppResponse(0), CUInt(CmdAppResponse.Length), ioRecvPci, receiveBuffer(0), receiveBufferLength)
        ListBox1.Items.Add("SCardTransmit() App Response: " + CStr(result) + ": Length " + CStr(receiveBufferLength))

        readFile1()

        SCardReleaseContext(hContext)

    End Sub

    Public Function readSegment(ByVal fileNumber As Integer, ByVal offset As Integer, ByVal length As Integer)
        Dim buffer(262) As Byte
        Dim responseLength As UInteger
        Dim result As UInteger

        responseLength = 2
        result = issueSetLengthRequest(length, buffer, responseLength)
        ListBox1.Items.Add("issueSetLengthRequest() App CmdSetLength: " + CStr(result) + ": Length " + CStr(responseLength))

        responseLength = 2
        result = issueSelectFileRequest(fileNumber, offset, length, buffer, responseLength)
        ListBox1.Items.Add("issueSelectFileRequest: " + CStr(result) + ": Length " + CStr(responseLength))

        responseLength = 254
        result = issueGetDataRequest(length, buffer, responseLength)

    End Function

    Public Sub readFile1()
        Dim content(459) As Byte
        Dim buffer(262) As Byte
        Dim responseLength As UInteger
        Dim result As UInteger

        responseLength = 2
        result = issueSetLengthRequest(252, buffer, responseLength)
        ListBox1.Items.Add("issueSetLengthRequest() App CmdSetLength: " + CStr(result) + ": Length " + CStr(responseLength))

        responseLength = 2
        result = issueSelectFileRequest(1, 0, 252, buffer, responseLength)
        ListBox1.Items.Add("issueSelectFileRequest: " + CStr(result) + ": Length " + CStr(responseLength))

        responseLength = 254
        result = issueGetDataRequest(252, buffer, responseLength)

    End Sub
    Private Function issueGetDataRequest(ByVal length As Byte, ByRef receiveBuffer As Byte(), ByRef bufferLength As UInteger)
        Dim result As UInteger = 0
        Dim cmd(5) As Byte
        cmd(0) = 204
        cmd(1) = 6
        cmd(2) = 0
        cmd(3) = 0
        cmd(4) = length
        result = SCardTransmit(hCard, ioSendPci, cmd(0), 5, ioRecvPci, receiveBuffer(0), bufferLength)
        Return result
    End Function

    Private Function issueSetLengthRequest(ByVal length As UInteger, ByRef receiveBuffer As Byte(), ByRef bufferLength As UInteger)
        Dim result As UInteger = 0
        Dim CmdSetLength(10) As Byte
        CmdSetLength(0) = 200
        CmdSetLength(1) = 50
        CmdSetLength(2) = 0
        CmdSetLength(3) = 0
        CmdSetLength(4) = 5
        CmdSetLength(5) = 8
        CmdSetLength(6) = 0
        CmdSetLength(7) = 0
        CmdSetLength(8) = CByte(length)
        CmdSetLength(9) = 0
        result = SCardTransmit(hCard, ioSendPci, CmdSetLength(0), 10, ioRecvPci, receiveBuffer(0), bufferLength)
        Return result
    End Function

    Private Function issueSelectFileRequest(ByVal fileNumber As Byte, ByVal offset As Integer, ByVal length As Byte, ByRef receiveBuffer As Byte(), ByRef bufferLength As UInteger)
        Dim CmdSelectFile(13) As Byte
        Dim result As Integer
        CmdSelectFile(0) = 204
        CmdSelectFile(1) = 0
        CmdSelectFile(2) = 0
        CmdSelectFile(3) = 0
        CmdSelectFile(4) = 8
        CmdSelectFile(5) = fileNumber Mod 256
        CmdSelectFile(6) = fileNumber / 256
        CmdSelectFile(7) = 1
        CmdSelectFile(8) = 0
        CmdSelectFile(9) = offset Mod 256
        CmdSelectFile(10) = offset / 256
        CmdSelectFile(11) = length
        CmdSelectFile(12) = 0
        result = SCardTransmit(hCard, ioSendPci, CmdSelectFile(0), 13, ioRecvPci, receiveBuffer(0), bufferLength)
        Return result
    End Function


End Class