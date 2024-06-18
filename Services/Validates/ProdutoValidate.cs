using TF_PP.Services.DTOs;
using TF_PP.Services.Exceptions;
using TF_PP.Services.Execptions;

namespace TF_PP.Services.Validate
{
    public class ProdutosValidate
    {
        public static void NumberValidadate(string barcode)
        {
            foreach (char c in barcode)
            {
                if (!(char.IsNumber(c)))
                    throw new BadRequestException("Para o tipo de barcode só poderá ter números de 0 a 9");
            }
        }
        public static void ValidateEAN13(string barcode)
        {
            if (barcode.Length != 13)
                throw new BadRequestException("Para o tipo de barcode EAN-13 deve ter 13 caracteres");

            NumberValidadate(barcode);
        }
        public static void ValidateDUN14(string barcode)
        {
            if (barcode.Length != 14)
                throw new BadRequestException("Para o tipo de barcode DUN-14 deve ter 14 caracteres");

            NumberValidadate(barcode);
        }
        public static void ValidateUPC(string barcode)
        {
            if (barcode.Length != 12)
                throw new BadRequestException("Para o tipo de barcode UPC deve ter 12 caracteres");

            NumberValidadate(barcode);
        }
        public static void ValidateCode11(string barcode)
        {
            if (barcode.Length != 11)
                throw new BadRequestException("Para o tipo de barcode CODE 11 deve ter 11 caracteres");

            foreach (char c in barcode)
            {
                if (!(char.IsDigit(c) || c == '-' || c == '*'))
                    throw new BadRequestException("Para o tipo de barcode CODE 11 só poderá ter números de 0 a 9, – e *");
            }
        }
        public static void ValidateCode39(string barcode)
        {
            if (barcode.Length > 39)
                throw new BadRequestException("Para o tipo de barcode CODE 39 deve ter até 39 caracteres");

            if (barcode.Length == 0 || barcode[0] != '*' || barcode[barcode.Length - 1] != '*')
                throw new BadRequestException("O primeiro e o último caractere devem ser '*'");

            foreach (char c in barcode)
            {
                if (!(char.IsLetterOrDigit(c) || c == '-' || c == '.' || c == '$' || c == '/' || c == '+' || c == '%' || c == ' ' || c == '*'))
                    throw new BadRequestException("Para o tipo de barcode CODE 39 só poderá ter Letras (A a Z), numéros (0 a 9) e (-, ., $, /, +, %, e espaço)"); ;
            }
        }

        private static void ValidateStringLength(ProdutoDTO dto)
        {
            if (dto.Description.Length > 255)
                throw new BadRequestException("Tamanho da descrição não pode exceder 255 caracteres");
            if (dto.Barcode.Length > 40)
                throw new BadRequestException("Tamanho do barcode não pode exceder 40 caracteres");
            if (dto.Barcodetype.Length > 10)
                throw new BadRequestException("Tamanho do tipo do barcode não pode exceder 10 caracteres");
        }

        public static bool Execute(ProdutoDTO dto)
        {
            ValidateStringLength(dto);

            if (dto.Barcodetype.ToUpper() == "EAN-13")
                ValidateEAN13(dto.Barcode);
            else if (dto.Barcodetype.ToUpper() == "DUN-14")
                ValidateDUN14(dto.Barcode);
            else if (dto.Barcodetype.ToUpper() == "UPC")
                ValidateUPC(dto.Barcode);
            else if (dto.Barcodetype.ToUpper() == "CODE 11")
                ValidateCode11(dto.Barcode);
            else if (dto.Barcodetype.ToUpper() == "CODE 39")
                ValidateCode39(dto.Barcode);
            else
                throw new InvalidEntityException("Tipo de barcode inválido para a gravação");

            return true;
        }
    }
}