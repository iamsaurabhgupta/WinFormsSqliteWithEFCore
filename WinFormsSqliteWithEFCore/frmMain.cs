using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsSqliteWithEFCore.Model;

namespace WinFormsSqliteWithEFCore
{
    public partial class frmMain : Form
    {
        private bool IsUpdate = false;
        private int Id = 0;
        public frmMain()
        {
            InitializeComponent();
        }

        private async void btn_Save_Click(object sender, EventArgs e)
        {
            using (var context = new UserContext())
            {
                if (!IsUpdate)
                {
                    UserDetails userDetails = new UserDetails();
                    userDetails.Name = txt_Name.Text.Trim();
                    userDetails.Email = txt_EmailId.Text.Trim();
                    userDetails.Contact = txt_Contact.Text.Trim();
                    context.UserDetails.Add(userDetails);
                    await context.SaveChangesAsync();
                }
                else
                {
                    UserDetails userDetails = context.UserDetails.FirstOrDefault(x => x.Id == Id);
                    userDetails.Name = txt_Name.Text.Trim();
                    userDetails.Email = txt_EmailId.Text.Trim();
                    userDetails.Contact = txt_Contact.Text.Trim();
                    context.Entry(userDetails).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
                this.ClearForm();
                dgv_UserDetails.DataSource = context.UserDetails.ToList();
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            using (var context = new UserContext())
            {
                this.ClearForm();
                dgv_UserDetails.DataSource = context.UserDetails.ToList();
            }
        }

        private void ClearForm()
        {
            txt_Name.Text = string.Empty;
            txt_Contact.Text = string.Empty;
            txt_EmailId.Text = string.Empty;
            this.IsUpdate = false;
            this.Id = 0;

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.ClearForm();
        }

        private async void dgv_UserDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_UserDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewButtonCell))
            {
                DataGridViewButtonCell dataGridViewButtonCell = (DataGridViewButtonCell) dgv_UserDetails.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var id = (int) dgv_UserDetails.Rows[e.RowIndex].Cells[1].Value;

                if (dataGridViewButtonCell.Value.Equals("Edit"))
                {
                    using (var context = new UserContext())
                    {
                        var userDetails = context.UserDetails.FirstOrDefault(x => x.Id == id);
                        txt_Contact.Text = userDetails.Contact;
                        txt_EmailId.Text = userDetails.Email;
                        txt_Name.Text = userDetails.Name;
                        this.Id = id;
                        this.IsUpdate = true;
                    }
                }
                else if (dataGridViewButtonCell.Value.Equals("Delete"))
                {
                    using (var context = new UserContext())
                    {
                        var userDetails = context.UserDetails.FirstOrDefault(x => x.Id == id);
                        context.UserDetails.Remove(userDetails);
                        context.Entry(userDetails).State = EntityState.Deleted;
                        await context.SaveChangesAsync();
                        dgv_UserDetails.DataSource = context.UserDetails.ToList();
                    }
                }
            }
        }
    }
}
