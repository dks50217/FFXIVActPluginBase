using System.Globalization;

namespace FFXIV_ACT_BASE.Data
{
    public static class PlayerData
    {
        public static uint Code { get; private set; } = 0;
        public static string Name { get; private set; } = string.Empty;

        public static uint PetCode { get; private set; } = 0;
        public static string PetName { get; private set; } = string.Empty;

        public static bool SetPlayer(string[] log)
        {
            try
            {
                Code = uint.Parse(log[2], NumberStyles.HexNumber);
                Name = log[3];
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SetPet(string[] log)
        {
            try
            {
                if (Code <= 0)
                {
                    return false;
                }

                uint summonerCode = uint.Parse(log[6], NumberStyles.HexNumber);
                if (Code == summonerCode)
                {
                    PetCode = uint.Parse(log[2], NumberStyles.HexNumber);
                    PetName = log[3];

                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        public static bool RemovePet(string[] log)
        {
            try
            {
                if (Code <= 0 || PetCode <= 0)
                {
                    return false;
                }

                uint summonerCode = uint.Parse(log[6], NumberStyles.HexNumber);
                uint logCode = uint.Parse(log[2], NumberStyles.HexNumber);
                if (Code == summonerCode && PetCode == logCode)
                {
                    PetCode = 0;
                    PetName = string.Empty;

                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }
    }
}
