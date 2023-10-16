using System.Collections.Generic;
using System.Collections;
using Adventure.BuildingBlocks;
using Utils;
using static Utils.Constants;

// KeyValue becomes an Alias for the spesific key value pair that we are using. Doing it this way to make the code more readable.
using KeyValue = System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.List<string>>;


namespace Adventure
{

    public class Parser
    {
        private const int ATTRIBUT_SEGMENT_MIN_SIZE = 2;
        bool isParsingComment = false;

        public Location CreateLocationFromDescription(string source)
        {
            Location location = new Location();
            HashSet<string> keywords = new();

            // Read the description file
            string[] rawLevel = Utils.FileUtils.ReadFromFile(source).Split(NEW_LINE);
            Dictionary<string, List<string>> allSegments = ExtractSegments(rawLevel);

            // For each segment in description create instances of items or update current location
            foreach (KeyValue segment in allSegments)
            {
                if (segment.Key == "location")
                {
                    location = (Location)Hydrate(segment, location, keywords);
                }
                else
                {
                    Item item = new Item();
                    item = (Item)Hydrate(segment, item, keywords);
                    location.Inventory.Add(segment.Key, item);
                }
            }

            location.keywords = keywords;

            return location;
        }

        object Hydrate(KeyValue segment, object target, HashSet<string> keywords)
        {
            for (int i = 0; i < segment.Value.Count; i++)
            {
                string directive = segment.Value[i];

                if (isAttribute(directive))
                {
                    target = UpdateWithAttribute(directive, target, keywords);
                }
                else if (isAction(directive))
                {
                    int actionIndex = 1;
                    string local = segment.Value[i + actionIndex].Trim();
                    int maxSegments = segment.Value.Count;
                    while (actionIndex < maxSegments && (local != "" && local.Contains("[") == false))
                    {
                        actionIndex++;
                        local = segment.Value[i + actionIndex].Trim();
                    }
                    List<String> actionDirective = segment.Value.GetRange(i, actionIndex);
                    target = UpdateWithAction(actionDirective, target, keywords);
                    i += (actionIndex - 1);
                }
            }

            return target;
        }

        private object UpdateWithAction(List<string> actionDescription, object instance, HashSet<string> keywords)
        {
            if (actionDescription.Count > 1)
            {
                string[] critieria = actionDescription[0].Replace("[", "").Replace("]", "").Split(".");
                string statusDesc = critieria[1].Trim();
                string actionDesc = critieria[2].Trim();
                string key = $"{statusDesc}.{actionDesc}";
                keywords.Add(actionDesc);

                string[] values = actionDescription.ToArray<string>()[1..]; // [1..] is a range opperator, it lets us get values from 1 to the end ..

                ((Item)instance).actions.Add(key, values);
            }

            return instance;
        }

        private object UpdateWithAttribute(string attributeDiscription, object instance, HashSet<string> keywords)
        {

            string[] parts = attributeDiscription.Split(new string[] { " = " }, StringSplitOptions.TrimEntries);
            string key = parts[0];
            string value = parts[1];

            // This is a tiny bit advanced, but not complicated. 
            // I am using a teqnique called reflection, it allows me to reason about code and the shape of objects when the program is runing.
            // It allows me to write simpler and cleaner code than the alternative. 
            // The question mark works as an if test (if the thing on the left of the ? is null, then the thing on the right side will not be run)
            // ? is how nullability is marked in c# (i.e that we beforhand say that somthing might be null and you should test.)
            instance.GetType().GetProperty(key)?.SetValue(instance, value);

            return instance;
        }

        Dictionary<string, List<string>> ExtractSegments(string[] levelDefinitionLines)
        {
            Dictionary<string, List<string>> segments = new Dictionary<string, List<string>>();
            List<string> segment = null;
            String segmentKey = "";

            for (int index = 0; index < levelDefinitionLines.Length; index++)
            {
                string line = levelDefinitionLines[index];

                if (isComment(line)) // If we are in a comment section we just want to skip this line. 
                {
                    continue; // continue is similar to break, only it just skips the current iteration (the current line) and dos not exit the loop.
                }

                if (line.StartsWith("["))
                {
                    if (segment != null)
                    {
                        segments.Add(segmentKey, segment);
                    }
                    segment = new List<string>();
                    segmentKey = line.Replace("[", "").Replace("]", "");
                }
                else
                {
                    segment.Add(line);
                }
            }

            if (segment != null)
            {
                segments.Add(segmentKey, segment);
            }
            return segments;
        }

        bool isComment(String line)
        {
            if (line.StartsWith("/*") || line.StartsWith("*/"))
            {
                isParsingComment = !isParsingComment;
            }

            return isParsingComment;
        }

        bool isAttribute(String line)
        {
            string[] parts = line.Split(" = ");
            return parts.Length >= ATTRIBUT_SEGMENT_MIN_SIZE;
        }

        bool isAction(string line)
        {
            var local = line.Trim();
            bool isaction = local.StartsWith("[Action.");
            return isaction;
        }

    }


}