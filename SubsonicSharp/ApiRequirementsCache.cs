using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SubsonicSharp.ActionGroups;

namespace SubsonicSharp
{
    internal static class ApiRequirementsCache
    {
        private static Dictionary<string, int> reqs = null;

        public static Dictionary<string, int> MethodApiRequirements
        {
            get { return reqs ?? ( reqs = GetRequirementsAllSubsonicTypes() ); }
        }

        public static Dictionary<string, int> GetApiRequirementsForTypeMethods(Type t)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            IEnumerable<MethodInfo> declaredMethods = t.GetTypeInfo().DeclaredMethods;
            foreach (MethodInfo info in declaredMethods)
            {
                foreach (CustomAttributeData attribute in info.CustomAttributes)
                {
                    if (attribute.AttributeType == typeof(ApiLevel))
                    {
                        dict.Add(info.Name, (int) attribute.ConstructorArguments[0].Value);
                    }
                }
            }
            return dict;
        }

        public static Dictionary<string, int> GetRequirementsAllSubsonicTypes()
        {
            List<Type> types = new List<Type>
            {
                typeof(Bookmarks),
                typeof(Chat),
                typeof(ClientBrowser),
                typeof(InformationLists),
                typeof(MediaAnnotation),
                typeof(MediaRetrieval),
                typeof(Playlists),
                typeof(Podcasts),
                typeof(Search),
                typeof(Sharing),
                typeof(UserManagement),
                typeof(SubsonicClient)
            };
            Dictionary<string, int> results = null;
            foreach (Type type in types)
            {
                Dictionary<string, int> typeReqs = GetApiRequirementsForTypeMethods(type);
                results =
                    results == null
                        ? typeReqs
                        : results.Union(typeReqs).ToDictionary(s => s.Key, s => s.Value);
            }
            return results;
        }
    }
}