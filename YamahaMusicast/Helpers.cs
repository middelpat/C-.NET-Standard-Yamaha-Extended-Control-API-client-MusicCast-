using System;

namespace YamahaMusicCast
{
    internal static class Helpers
    {
        internal static T[] GetEnumValues<T>()
        {
            return ((T[])Enum.GetValues(typeof(T)));
        }


        internal static string GetMessageForResponseCode(int responseCode)
        {
            switch(responseCode)
            {
                case 0: return "Success";
                case 1: return "Initializing";
                case 2: return "Internal error";
                case 3: return "Invalid Request (A method did not exist, a method wasn’t appropriate etc.)";
                case 4: return "Invalid Parameter (Out of range, invalid characters etc.)";
                case 5: return "Guarded (Unable to setup in current status etc.)";
                case 6: return "Time Out";
                case 99: return "Firmware updating";

                //(100s are Streaming Service related errors)
                case 100: return "Streaming service: Access Error";
                case 101: return "Streaming service: Other errors";
                case 102: return "Streaming service: Incorrect username";
                case 103: return "Streaming service: Incorrect password";
                case 104: return "Streaming service: Account expired";
                case 105: return "Streaming service: Account Disconnected/Gone Off/Shut Down";
                case 106: return "Streaming service: Account Number Reached to the Limit";
                case 107: return "Streaming service: Server Maintenance";
                case 108: return "Streaming service: Invalid Account";
                case 109: return "Streaming service: License Error";
                case 110: return "Streaming service: Read-only mode";
                case 111: return "Streaming service: Max stations";
                case 112: return "Streaming service: Access Denied";

                default:
                    return "Unknown";
            }
        }
    }
}
