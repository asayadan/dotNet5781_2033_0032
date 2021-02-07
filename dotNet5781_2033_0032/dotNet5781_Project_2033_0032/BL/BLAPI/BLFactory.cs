
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
                    return new BLImp();
                default:
                    return new BLImp();
            }
        }
    }
}
