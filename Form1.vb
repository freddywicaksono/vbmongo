Imports MongoDB.Driver
Imports MongoDB.Bson
Public Class Form1
    Private InsertedDoc As New BsonDocument
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ReloadData()

    End Sub

    Private Sub ReloadData()
        oMhs = db.GetCollection(Of BsonDocument)("mahasiswa")
        docs = oMhs.Find(New BsonDocument()).ToList()

        Dim dt = New DataTable()

        dt.Columns.Add("nim")
        dt.Columns.Add("nama")
        dt.Columns.Add("email")

        For Each item As MongoDB.Bson.BsonDocument In docs
            Dim R As DataRow = dt.NewRow

            Dim nim As BsonElement = item.GetElement("nim")
            Dim nama As BsonElement = item.GetElement("nama")
            Dim email As BsonElement = item.GetElement("email")

            R("nim") = nim.Value
            R("nama") = nama.Value
            R("email") = email.Value
            dt.Rows.Add(R)
        Next
        grid.DataSource = dt
    End Sub

    Private Sub txtnim_KeyDown(sender As Object, e As KeyEventArgs) Handles txtnim.KeyDown
        Dim s As String
        oMhs = db.GetCollection(Of BsonDocument)("mahasiswa")
        If (e.KeyCode = Keys.Enter) Then
            'Query:
            '--------------------------------
            s = "{nim:" & txtnim.Text & "}"
            '--------------------------------
            docs = oMhs.Find(s).ToList()
            For Each item As MongoDB.Bson.BsonDocument In docs

                Dim nim As BsonElement = item.GetElement("nim")
                Dim nama As BsonElement = item.GetElement("nama")
                Dim email As BsonElement = item.GetElement("email")


                txtnama.Text = nama.Value
                txtemail.Text = email.Value
            Next

            If (docs.Count = 0) Then
                MessageBox.Show("Data Tidak Ditemukan")
                barang_baru = True
            Else
                barang_baru = False
                txtnama.Focus()
            End If

        End If
    End Sub
   
    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If (barang_baru = True) Then
            Simpan()
        Else
            UpdateData()
        End If
        
    End Sub

    Private Sub Simpan()

        oMhs = db.GetCollection(Of BsonDocument)("mahasiswa")

        Dim doc = New BsonDocument From {
            {"nim", Convert.ToInt32(txtnim.Text)},
            {"nama", txtnama.Text},
            {"email", txtemail.Text}
        }

        Try
            oMhs.InsertOne(doc)
            MessageBox.Show("Data berhasil disimpan")
        Catch ex As MongoDB.Driver.MongoWriteException
            MessageBox.Show(ex.ToString)
        End Try

        ClearEntry()
        ReloadData()
    End Sub

    Private Sub UpdateData()


        oMhs = db.GetCollection(Of BsonDocument)("mahasiswa")
        
        Dim filter = Builders(Of BsonDocument).Filter.Eq(Of Integer)("nim", Convert.ToInt32(txtnim.Text))
        Dim update = Builders(Of BsonDocument).Update.Set(Of String)("nama", txtnama.Text)
        Dim update2 = Builders(Of BsonDocument).Update.Set(Of String)("email", txtemail.Text)



        oMhs.UpdateOne(filter, update)
        oMhs.UpdateOne(filter, update2)

        MessageBox.Show("Data berhasil diubah")

        ClearEntry()
        ReloadData()
    End Sub
    
    Private Sub ClearEntry()
        txtnim.Clear()
        txtnama.Clear()
        txtemail.Clear()
        txtnim.Focus()
    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        Dim s As String
        Dim jawab As Integer
        s = "{nim:" & txtnim.Text & "}"

        jawab = MessageBox.Show("Apakah data ini akan dihapus", "Konfirmasi", MessageBoxButtons.YesNo)
        If jawab = vbYes Then
            oMhs.DeleteOne(s)
            MessageBox.Show("Data telah dihapus")
            ClearEntry()
            ReloadData()
        Else
            MessageBox.Show("Data batal dihapus")
        End If
    End Sub
End Class
