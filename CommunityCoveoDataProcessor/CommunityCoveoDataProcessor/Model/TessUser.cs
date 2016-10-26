using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Evolution.Extensibility.OAuthClient.Version1;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace CommunityCoveoDataProcessor.Model
{
    public class User
        {
            private object _syncRoot = new object();

            internal static User Empty;

            public string LanguageKey
            {
                get;
                private set;
            }

            public string OAuthToken
            {
                get;
                set;
            }

            internal string RefreshToken
            {
                get;
                set;
            }

            internal string SynchronizedUserName
            {
                get;
                set;
            }

            internal object SyncRoot
            {
                get
                {
                    return this._syncRoot;
                }
            }

            internal DateTime TokenExpiresUtc
            {
                get;
                set;
            }

            public int UserId
            {
                get;
                private set;
            }

            public string UserName
            {
                get;
                private set;
            }

            public User()
            {
                User.Empty = new User(null, 0, null, null);
            }

            public  User(string userName, int userId, string languageKey, string oAuthToken)
            {
                this.UserName = userName;
                this.UserId = userId;
                this.LanguageKey = languageKey;
                this.OAuthToken = OAuthToken;
            }

            internal static User Deserialize(string serializedUser, string signature)
            {
                string[] strArrays = serializedUser.Split(new char[] { ':' });
                if ((int)strArrays.Length != 2)
                {
                    return null;
                }
                byte[] numArray = Convert.FromBase64String(strArrays[0]);
                byte[] numArray1 = Convert.FromBase64String(strArrays[1]);
                HMACSHA256 hMACSHA256 = new HMACSHA256(Encoding.UTF8.GetBytes(signature));
                byte[] numArray2 = hMACSHA256.ComputeHash(numArray1);
                bool flag = false;
                if ((int)numArray2.Length == (int)numArray.Length)
                {
                    for (int i = 0; i < (int)numArray.Length && numArray[i] == numArray2[i]; i++)
                    {
                        if (i == (int)numArray.Length - 1)
                        {
                            flag = true;
                        }
                    }
                }
                if (!flag)
                {
                    return null;
                }
                string[] strArrays1 = Encoding.UTF8.GetString(numArray1).Split(new char[] { '?' });
                if ((int)strArrays1.Length != 7)
                {
                    return null;
                }
                User user = new User(Uri.UnescapeDataString(strArrays1[0]), int.Parse(Uri.UnescapeDataString(strArrays1[1])), Uri.UnescapeDataString(strArrays1[2]), null)
                {
                    SynchronizedUserName = Uri.UnescapeDataString(strArrays1[3]),
                    OAuthToken = Uri.UnescapeDataString(strArrays1[4]),
                    RefreshToken = Uri.UnescapeDataString(strArrays1[5]),
                    TokenExpiresUtc = new DateTime(long.Parse(Uri.UnescapeDataString(strArrays1[6])), DateTimeKind.Utc)
                };
                return user;
            }

            public override bool Equals(object obj)
            {
                User user = obj as User;
                if (user == null)
                {
                    return false;
                }
                if (user.UserName != null || this.UserName != null || user.UserId != 0)
                {
                    return false;
                }
                return this.UserId == 0;
            }

            public override int GetHashCode()
            {
                return this.GetHashCode();
            }

            internal string Serialize(string signature)
            {
                HMACSHA256 hMACSHA256 = new HMACSHA256(Encoding.UTF8.GetBytes(signature));
                Encoding uTF8 = Encoding.UTF8;
                string[] strArrays = new string[13];
                strArrays[0] = Uri.EscapeDataString(this.UserName);
                strArrays[1] = "?";
                int userId = this.UserId;
                strArrays[2] = Uri.EscapeDataString(userId.ToString("0"));
                strArrays[3] = "?";
                strArrays[4] = Uri.EscapeDataString(this.LanguageKey);
                strArrays[5] = "?";
                strArrays[6] = Uri.EscapeDataString(this.SynchronizedUserName ?? string.Empty);
                strArrays[7] = "?";
                strArrays[8] = Uri.EscapeDataString(this.OAuthToken);
                strArrays[9] = "?";
                strArrays[10] = Uri.EscapeDataString(this.RefreshToken);
                strArrays[11] = "?";
                long ticks = this.TokenExpiresUtc.Ticks;
                strArrays[12] = Uri.EscapeDataString(ticks.ToString());
                byte[] bytes = uTF8.GetBytes(string.Concat(strArrays));
                return string.Concat(Convert.ToBase64String(hMACSHA256.ComputeHash(bytes)), ":", Convert.ToBase64String(bytes));
            }
        }
    }

