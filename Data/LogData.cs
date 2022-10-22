using FFXIV_ACT_BASE.Model;
using System.Globalization;

namespace FFXIV_ACT_BASE.Data
{
    public readonly struct LogData
    {
        public readonly bool IsByMe;
        public readonly GameIdx GameIdx;
        public readonly bool IsValid;
        
        public LogData(string[] log)
        {
            try
            {
                uint casterCode = uint.Parse(log[2], NumberStyles.HexNumber);
                GameIdx = (GameIdx)int.Parse(log[4], NumberStyles.HexNumber);
                IsByMe = (PlayerData.Code == casterCode) || (PlayerData.PetCode == casterCode);
                IsValid = IsByMe;
            }
            catch
            {
                IsByMe = false;
                GameIdx = (GameIdx)(-1);
                IsValid = false;
            }
        }
    }
}
