using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blitSub.Domain.Models
{
    [Serializable]
    public class Version
    {
        private int major;
        private int minor;
        private int beta;
        private int bugfix;

        public Version()
        {
            // For Kryo
        }

        /**
         * Creates a new version instance by parsing the given string.
         * @param version A string of the format "1.27", "1.27.2" or "1.27.beta3".
         */
        public Version(string version)
        {
            string[] s = version.Split("\\.");
            major = int.Parse(s[0]);
            minor = int.Parse(s[1]);

            if (s.Length > 2)
            {
                if (s[2].Contains("beta"))
                {
                    beta = int.Parse(s[2].Replace("beta", ""));
                }
                else
                {
                    bugfix = int.Parse(s[2]);
                }
            }
        }

        public int getMajor()
        {
            return major;
        }

        public int getMinor()
        {
            return minor;
        }

        public string getVersion()
        {
            switch (major)
            {
                case 1:
                    switch (minor)
                    {
                        case 0:
                            return "3.8";
                        case 1:
                            return "3.9";
                        case 2:
                            return "4.0";
                        case 3:
                            return "4.1";
                        case 4:
                            return "4.2";
                        case 5:
                            return "4.3.1";
                        case 6:
                            return "4.5";
                        case 7:
                            return "4.6";
                        case 8:
                            return "4.7";
                        case 9:
                            return "4.8";
                        case 10:
                            return "4.9";
                        case 11:
                            return "5.1";
                        case 12:
                            return "5.2";
                        case 13:
                            return "5.3";
                        case 14:
                            return "6.0";
                    }
                    break;
            }
            return "";
        }

        /**
         * Return whether this object is equal to another.
         * @param o Object to compare to.
         * @return Whether this object is equals to another.
         */
        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (o == null || GetType() != o.GetType()) return false;

            var version = (Version)o;

            if (beta != version.beta) return false;
            if (bugfix != version.bugfix) return false;
            if (major != version.major) return false;
            return minor == version.minor;
        }

        /**
         * Returns a hash code for this object.
         * @return A hash code for this object.
         */
        public override int GetHashCode()
        {
            int result;
            result = major;
            result = 29 * result + minor;
            result = 29 * result + beta;
            result = 29 * result + bugfix;
            return result;
        }

        /**
         * Returns a string representation of the form "1.27", "1.27.2" or "1.27.beta3".
         * @return A string representation of the form "1.27", "1.27.2" or "1.27.beta3".
         */
        public override string ToString()
        {
            var buf = new StringBuffer();
            buf.append(major).append('.').append(minor);
            if (beta != 0)
            {
                buf.append(".beta").append(beta);
            }
            else if (bugfix != 0)
            {
                buf.append('.').append(bugfix);
            }

            return buf.toString();
        }

        /**
         * Compares this object with the specified object for order.
         * @param version The object to compare to.
         * @return A negative integer, zero, or a positive integer as this object is less than, equal to, or
         * greater than the specified object.
         */
        
        public int CompareTo(Version version)
        {
            if (major < version.major)
            {
                return -1;
            }
            else if (major > version.major)
            {
                return 1;
            }

            if (minor < version.minor)
            {
                return -1;
            }
            else if (minor > version.minor)
            {
                return 1;
            }

            if (bugfix < version.bugfix)
            {
                return -1;
            }
            else if (bugfix > version.bugfix)
            {
                return 1;
            }

            int thisBeta = beta == 0 ? int.MaxValue : beta;
            int otherBeta = version.beta == 0 ? int.MaxValue : version.beta;

            if (thisBeta < otherBeta)
            {
                return -1;
            }
            else if (thisBeta > otherBeta)
            {
                return 1;
            }

            return 0;
        }
    }
}
