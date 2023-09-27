

namespace IBCL.Domain.Persistence
{
        public static class SequentialGuid
        {
            public static Guid NewSequentialGuid()
            {
                byte[] array = Guid.NewGuid().ToByteArray();
                DateTime now = DateTime.Now;
                array[3] = (byte)now.Year;
                array[2] = (byte)now.Month;
                array[1] = (byte)now.Day;
                array[0] = (byte)now.Hour;
                array[5] = (byte)now.Minute;
                array[4] = (byte)now.Second;
                return new Guid(array);
            }
        }
    }

