using System;
using System.Windows.Forms;

namespace KChatManager
{
    public partial class PageSeparator : UserControl
    {
        private int totalNum;
        private int currentNum;

        public PageSeparator()
        {
            InitializeComponent();           
            update();
        }

        public void update(int num = 0)
        {
            totalNum = num;

            lbTotalNum.Text = totalNum.ToString();

            if (totalNum == 0)
            {
                txtPageNum.Text = "0";
            }
            else
            {
                txtPageNum.Text = "1";
            }

            currentNum = int.Parse(txtPageNum.Text);

            updateBtn(currentNum);
        }

        private void updateBtn(int currentNum)
        {
            if (currentNum <= 1)
            {
                btnFirstPage.Enabled = false;
                btnPrePage.Enabled = false;
            }
            if (totalNum <= 1)
            {
                btnFirstPage.Enabled = false;
                btnPrePage.Enabled = false;
                btnNextPage.Enabled = false;
                btnLastPage.Enabled = false;
            }
            if (currentNum == totalNum)
            {
                btnNextPage.Enabled = false;
                btnLastPage.Enabled = false;
            }
        }

        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            currentNum = 1;
            txtPageNum.Text = "1";
            updateBtn(currentNum);
        }

        private void btnPrePage_Click(object sender, EventArgs e)
        {
            currentNum -= 1;
            txtPageNum.Text = currentNum.ToString();
            updateBtn(currentNum);
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            currentNum += 1;
            txtPageNum.Text = currentNum.ToString();
            updateBtn(currentNum);
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            currentNum = totalNum;
            txtPageNum.Text = currentNum.ToString();
            updateBtn(currentNum);
        }
    }
}
