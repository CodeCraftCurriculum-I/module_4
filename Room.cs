namespace Adventure
{
    public static class Room
    {
        public static Location Initialize()
        {
            var students = new ActionSubject
            {
                Id = "students",
                Status = "Idle",
                Actions = new Dictionary<string, Effect>
                {
                    ["kick"] = new Effect
                    {
                        Description = "Getting kicked does not make them happy",
                        Action = (target, playerInput, location, player, changeLocation) =>
                        {
                            target.Status = "Angry";
                            if (target.Status == "Angry")
                            {
                                target.Status = "Enraged";
                                player.HitPoints -= 1;
                            }
                        }
                    }
                }
            };

            var key = new ActionSubject
            {
                Id = "magicKey",
                Description = "A key that is made of a compound so dark it drains the light around it.",
                Status = "OnFloor",
                Actions = new Dictionary<string, Effect>
                {
                    ["take"] = new Effect
                    {
                        Description = "You pick up the key and put it in your pocket",
                        Action = (subject, playerInput, location, player, changeLocation) =>
                        {
                            subject.Status = "Pocket";
                            player.Inventory.Add(subject);
                        }
                    },
                    ["throw"] = new Effect
                    {
                        Description = "You throw the key away",
                        Action = (subject, playerInput, location, player, changeLocation) =>
                        {
                            subject.Status = "OnFloor";
                            player.Inventory.Remove(subject);
                        }
                    }
                }
            };

            var door = new ActionSubject
            {
                Id = "door",
                Status = "Closed",
                Actions = new Dictionary<string, Effect>
                {
                    ["kick"] = new Effect
                    {
                        Description = "The door is of poor quality and splinters as your foot impacts it.",
                        Action = (subject, playerInput, location, player, changeLocation) =>
                        {
                            subject.Status = "Open";
                        }
                    },
                    ["go"] = new Effect
                    {
                        Description = "You try going through the door, but it is closed so you bump your head.",
                        Action = (subject, playerInput, location, player, changeLocation) =>
                        {
                            if (subject.Status == "Open")
                            {
                                changeLocation("hallway");
                            }
                        }
                    }
                }
            };

            var window = new ActionSubject
            {
                Id = "window",
                Status = "Broken",
                Actions = new Dictionary<string, Effect>
                {
                    ["default"] = new Effect
                    {
                        Description = "There is a bit of broken glass and a surprising amount of blood",
                        Action = (subject, playerInput, location, player, changeLocation) => { }
                    }
                }
            };

            return new Location()
            {
                Id = "start",
                Description = "You are standing in a room there is a key on the floor, a door, a window and some students",
                Subjects = new Dictionary<string, ActionSubject>
                {
                    ["key"] = key,
                    ["students"] = students,
                    ["door"] = door,
                    ["window"] = window
                }

            };

        }
    }
}