Public Class Form1
    Dim hp, hp_total, strenght, block, enemy_block, energy, totalenergy, gold, floor, cards_total, cards_available, cards_discarded, hp_enemy, hp_enemy_total, enemy_action, contor, dmg As Integer
    Dim turn, vulnerable, weak As Boolean
    Dim yname As String
    Function random() As Integer
        Randomize()
        Return Int((6 * Rnd()) + 1)
    End Function
    Function ydmg(ByVal a As Integer, ByVal b As Integer) As Integer
        Dim temp As Integer
        temp = -a + b
        If temp < 0 Then
            enemy_block -= b
            Return 0
        ElseIf temp >= 0 Then
            enemy_block = 0
            Return temp
        End If
    End Function
    Private Sub Form1_load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        My.Computer.Audio.Play(My.Resources.Exordium,
          AudioPlayMode.BackgroundLoop)
        hp = 50
        hp_total = 50
        energy = 3
        totalenergy = 3
        gold = 50
        floor = 1
        cards_total = 8
        cards_available = 3
        cards_discarded = 0
        hp_enemy = 232
        hp_enemy_total = 232
        enemy_action = random()
        yname = "Player"
        strenght = 0
        block = 0
        contor = 0
        enemy_block = 0
        ' energy
        Label1.Text = energy & "/" & totalenergy
        ' hp
        Label10.Text = hp & "/" & hp_total
        ' enemy hp
        Label9.Text = hp_enemy & "/" & hp_enemy_total
        ' cards in deck
        Label2.Text = cards_available
        ' cards discarded
        Label3.Text = cards_discarded
        ' total cards
        Label8.Text = cards_total
        ' gold
        Label6.Text = gold
        ' floor 
        Label7.Text = floor
        ' enemy action
        Label11.Text = enemy_block
        ' your name
        Label4.Text = yname
        ' your block
        Label12.Text = block
        ' enemy action
        If enemy_action = 1 Then
            Label13.Text = "6 dmg"
        ElseIf enemy_action = 2 Then
            Label13.Text = "12 block"
        ElseIf enemy_action = 3 Then
            Label13.Text = "9 block + 9 dmmg"
        ElseIf enemy_action = 4 Then
            Label13.Text = "block 8 + remove debuff"
        ElseIf enemy_action = 5 Then
            Label13.Text = "25 dmg"
        ElseIf enemy_action = 6 Then
            Label13.Text = "Fumble"
        End If
        ' Creating and initializing list
        Dim lst As List(Of Integer) = New List(Of Integer)()
       
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If energy > 0 Then
            energy -= 1
            If vulnerable Then
                hp_enemy -= ydmg(enemy_block, 6)
            Else
                hp_enemy -= ydmg(enemy_block, ((3 / 2) * 6))
            End If
            Label9.Text = hp_enemy & "/" & hp_enemy_total
            Label1.Text = energy & "/" & totalenergy
            Label11.Text = enemy_block

        Else
            MessageBox.Show("Not enough energy!")
        End If
        If hp_enemy <= 0 Then
            MessageBox.Show("Congratulations, the enemy is dead, the alpha is over!")

        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If energy > 0 Then
            energy -= 1
            block += 5
            Label12.Text = block
            Label1.Text = energy & "/" & totalenergy
        Else
            MessageBox.Show("Not enough energy!")
        End If
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If energy >= 2 Then
            energy -= 2
            If vulnerable Then
                hp_enemy -= ydmg(enemy_block, 8)
            Else
                hp_enemy -= ydmg(enemy_block, ((3 / 2) * 8))
            End If
            Label9.Text = hp_enemy & "/" & hp_enemy_total
            Label1.Text = energy & "/" & totalenergy
            Label11.Text = enemy_block
            vulnerable = False
        Else
            MessageBox.Show("Not enough energy!")
        End If
        If hp_enemy <= 0 Then
            MessageBox.Show("Congratulations, the enemy is dead, the alpha is over!")

        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If contor = 0 Then
            energy += 2
            hp -= 3
            contor += 1
            Label1.Text = energy & "/" & totalenergy
            Label10.Text = hp & "/" & hp_total
        ElseIf contor > 0 Then
            MessageBox.Show("Not enough energy!")
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If energy >= 2 Then
            energy -= 2
            If vulnerable Then
                hp_enemy -= ydmg(enemy_block, 12)
            Else
                hp_enemy -= ydmg(enemy_block, ((3 / 2) * 12))
            End If
            Label9.Text = hp_enemy & "/" & hp_enemy_total
            Label1.Text = energy & "/" & totalenergy
            Label11.Text = enemy_block
            weak = False
            If enemy_action = 1 And Not weak Then
                Label13.Text = "3 dmg"
            ElseIf enemy_action = 3 And Not weak Then
                Label13.Text = "9 block + 5 dmmg"
            ElseIf enemy_action = 5 And Not weak Then
                Label13.Text = "13 dmg"
            End If
        Else
            MessageBox.Show("Not enough energy!")
        End If
        If hp_enemy <= 0 Then
            MessageBox.Show("Congratulations, the enemy is dead, the alpha is over!")

        End If
    End Sub

    Private Sub End_turn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles End_turn.Click
        enemy_block = 0
        'aici stiu ca e de 2 ori acelasi cod si nu e ok, rezolv next time
        If weak Then
            If enemy_action = 1 Then
                If block >= 6 Then
                    block -= 6
                ElseIf block = 0 Then
                    hp -= 6
                ElseIf block < 6 Then
                    hp += (block - 6)
                End If
            ElseIf enemy_action = 2 Then
                enemy_block += 12
            ElseIf enemy_action = 3 Then
                enemy_block = 9
                If block >= 9 Then
                    block -= 9
                ElseIf block = 0 Then
                    hp -= 9
                ElseIf block < 9 Then
                    hp += (block - 9)
                End If
            ElseIf enemy_action = 4 Then
                enemy_block = 8
                vulnerable = True
                weak = True
            ElseIf enemy_action = 5 Then
                If block >= 25 Then
                    block -= 25
                ElseIf block = 0 Then
                    hp -= 25
                ElseIf block < 25 Then
                    hp += (block - 25)
                End If
            ElseIf enemy_action = 6 Then
                hp_enemy -= 5
                Label9.Text = hp_enemy & "/" & hp_enemy_total
            End If
        ElseIf weak = False Then
            If enemy_action = 1 Then
                If block >= 3 Then
                    block -= 3
                ElseIf block = 0 Then
                    hp -= 3
                ElseIf block < 3 Then
                    hp += (block - 3)
                End If
            ElseIf enemy_action = 2 Then
                enemy_block += 12
            ElseIf enemy_action = 3 Then
                enemy_block = 9
                If block >= 5 Then
                    block -= 5
                ElseIf block = 0 Then
                    hp -= 5
                ElseIf block < 5 Then
                    hp += (block - 5)
                End If
            ElseIf enemy_action = 4 Then
                enemy_block = 8
                vulnerable = True
                weak = True
            ElseIf enemy_action = 5 Then
                If block >= 13 Then
                    block -= 13
                ElseIf block = 0 Then
                    hp -= 13
                ElseIf block < 13 Then
                    hp += (block - 13)
                End If
            ElseIf enemy_action = 6 Then
                hp_enemy -= 3
                Label9.Text = hp_enemy & "/" & hp_enemy_total
            End If
        End If
        energy = 3
        block = 0
        contor = 0
        Label1.Text = energy & "/" & totalenergy
        Label10.Text = hp & "/" & hp_total
        Label12.Text = block
        Label11.Text = enemy_block
        enemy_action = random()
        If enemy_action = 1 Then
            Label13.Text = "6 dmg"
        ElseIf enemy_action = 2 Then
            Label13.Text = "12 block"
        ElseIf enemy_action = 3 Then
            Label13.Text = "9 block + 9 dmmg"
        ElseIf enemy_action = 4 Then
            Label13.Text = "block 8 + remove debuff"
        ElseIf enemy_action = 5 Then
            Label13.Text = "25 dmg"
        ElseIf enemy_action = 6 Then
            Label13.Text = "Fumble"
        End If
        If enemy_action = 1 And Not weak Then
            Label13.Text = "3 dmg"
        ElseIf enemy_action = 3 And Not weak Then
            Label13.Text = "9 block + 5 dmmg"
        ElseIf enemy_action = 5 And Not weak Then
            Label13.Text = "13 dmg"
        End If
    End Sub
End Class
