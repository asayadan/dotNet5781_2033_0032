
using BL;

namespace BLAPI
{
    public static class BLFactory
    {
        public static IBL GetBL(string type)
        {
            switch (type)

            {
                case "BLImp":
                    return BLImp.Instance;
                default:
                    return BLImp.Instance;
            }
        }
    }
}
