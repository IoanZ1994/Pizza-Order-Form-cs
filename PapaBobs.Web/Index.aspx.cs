using System;

namespace PapaBobs.Web
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void orderButton_Click(object sender, EventArgs e)
        {
            if (!textBoxDataIsValid())
                return;
            else
                validationLabel.Text = "";

            var orderDTO = buildOrder();

            Domain.OrderManager.CreateOrder(orderDTO);
            Response.Redirect("Success.aspx");
        }

        private bool textBoxDataIsValid()
        {
            if (nameTextBox.Text.Trim().Length == 0)
            {
                textBoxValidationError("name");
                return false;
            }
            else if (addressTextBox.Text.Trim().Length == 0)
            {
                textBoxValidationError("address");
                return false;
            }
            else if (zipCodeTextBox.Text.Trim().Length == 0)
            {
                textBoxValidationError("zip code");
                return false;
            }
            else if (phoneTextBox.Text.Trim().Length == 0)
            {
                textBoxValidationError("phone number");
                return false;
            }
            else
                return true;
        }

        private DTO.Enums.SizeType determineSize()
        {
            DTO.Enums.SizeType size;

            if (!Enum.TryParse(sizeDropDown.SelectedValue, out size))
            {
                throw new Exception("Could not determine Pizza size.");
            }

            return size;
        }

        private DTO.Enums.CrustType determineCrust()
        {
            DTO.Enums.CrustType crust;

            if (!Enum.TryParse(crustDropDown.SelectedValue, out crust))
            {
                throw new Exception("Could not determine Pizza crust.");
            }

            return crust;
        }

        private DTO.Enums.PaymentType determinePayment()
        {
            DTO.Enums.PaymentType paymentMethod;
            if (cashRadio.Checked)
                paymentMethod = DTO.Enums.PaymentType.Cash;
            else paymentMethod = DTO.Enums.PaymentType.Credit;
            return paymentMethod;
        }

        private void textBoxValidationError(string errorType)
        {
            string errorMessage = "";
            errorMessage += string.Format("<strong>Please enter a {0}.</strong>", errorType);
            validationLabel.Text = errorMessage;
            validationLabel.Visible = true;
        }

        protected void recalculateTotalCost(object sender, EventArgs e)
        {
            if (sizeDropDown.SelectedValue == "")
                return;
            if (crustDropDown.SelectedValue == "")
                return;

            var order = buildOrder();
            decimal cost = Domain.PizzaPriceManager.CalculateCost(order);
            resultLabel.Text = string.Format("<h3>{0:C}</h3>", cost);
        }

        private DTO.OrderDTO buildOrder()
        {
            PapaBobs.DTO.OrderDTO orderDTO = new DTO.OrderDTO();

            orderDTO.Size = determineSize();
            orderDTO.Crust = determineCrust();
            orderDTO.Sausage = sausageCheckBox.Checked;
            orderDTO.Pepperoni = pepperoniCheckBox.Checked;
            orderDTO.Onions = onionCheckBox.Checked;
            orderDTO.GreenPeppers = greenPepperCheckBox.Checked;
            orderDTO.Name = nameTextBox.Text;
            orderDTO.Address = addressTextBox.Text;
            orderDTO.ZipCode = zipCodeTextBox.Text;
            orderDTO.Phone = phoneTextBox.Text;
            orderDTO.PaymentType = determinePayment();
            orderDTO.TotalCost = 16.50M;

            return orderDTO;
        }
    }
}