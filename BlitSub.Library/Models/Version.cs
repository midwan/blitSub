using System;
using System.Collections.Generic;
using System.Text;

namespace BlitSub.Library.Models
{
    [Serializable]
    public class Version
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Beta { get; set; }
        public int Bugfix { get; set; }

        public Version()
        {

        }

        public Version(string version)
        {
            var s = version.Split("\\.");
            Major = int.Parse(s[0]);
            Minor = int.Parse(s[1]);

            if (s.Length > 2)
            {
                if (s[2].Contains("beta"))
                {
                    Beta = int.Parse(s[2].Replace("beta", ""));
                }
                else
                {
                    Bugfix = int.Parse(s[2]);
                }
            }
        }

        public string GetVersion()
        {
            switch (Major)
            {
                case 1:
                    switch (Minor)
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

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (o == null || GetType() != o.GetType()) return false;

            var version = (Version)o;

            if (Beta != version.Beta) return false;
            if (Bugfix != version.Bugfix) return false;
            if (Major != version.Major) return false;
            return Minor == version.Minor;
        }

        public override int GetHashCode()
        {
            int result;
            result = Major;
            result = 29 * result + Minor;
            result = 29 * result + Beta;
            result = 29 * result + Bugfix;
            return result;
        }

        public override string ToString()
        {
            var buf = new StringBuilder();
            buf.Append(Major).Append('.').Append(Minor);
            if (Beta != 0)
            {
                buf.Append(".beta").Append(Beta);
            }
            else if (Bugfix != 0)
            {
                buf.Append('.').Append(Bugfix);
            }

            return buf.ToString();
        }

        public int CompareTo(Version version)
        {
            if (Major < version.Major)
            {
                return -1;
            }

            if (Major > version.Major)
            {
                return 1;
            }

            if (Minor < version.Minor)
            {
                return -1;
            }

            if (Minor > version.Minor)
            {
                return 1;
            }

            if (Bugfix < version.Bugfix)
            {
                return -1;
            }

            if (Bugfix > version.Bugfix)
            {
                return 1;
            }

            var thisBeta = Beta == 0 ? int.MaxValue : Beta;
            var otherBeta = version.Beta == 0 ? int.MaxValue : version.Beta;

            if (thisBeta < otherBeta)
            {
                return -1;
            }

            if (thisBeta > otherBeta)
            {
                return 1;
            }

            return 0;
        }
    }
}
