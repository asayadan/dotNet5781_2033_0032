
using BL;

namespace BLAPI
{
    public static class BLFactory
    {
        public static IBL GetBL(string type)
        {
            switch (type)
            {
                case "1":
                    return new BLImp();
                default:
                    return new BLImp();
            }
        }
    }
}
