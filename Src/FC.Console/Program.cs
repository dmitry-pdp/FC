using FC.Core.Common;
using FC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FC.Core
{
    class Program
    {
        private static Random rand = new Random();

        static void Main(string[] args)
        {
            // Create world

            var ship1 = new Frigate(objectId: 1, objectTypeId: 1, armor: 200, resistance: 40f);
            var ship2 = new Frigate(objectId: 2, objectTypeId: 1, armor: 200, resistance: 40f);
            var ship3 = new Frigate(objectId: 3, objectTypeId: 1, armor: 200, resistance: 40f);
            var ship4 = new Frigate(objectId: 4, objectTypeId: 1, armor: 200, resistance: 40f);
            var ship5 = new Frigate(objectId: 5, objectTypeId: 2, armor: 300, resistance: 50f);
            var ship6 = new Frigate(objectId: 6, objectTypeId: 2, armor: 300, resistance: 50f);
            var ship7 = new Frigate(objectId: 7, objectTypeId: 2, armor: 300, resistance: 50f);
            var ship8 = new Frigate(objectId: 8, objectTypeId: 1, armor: 200, resistance: 40f);
            var fleet1 = new Fleet(objectId: 10, objectTypeId: 5) { Ships = new[] { ship1, ship2, ship3, ship4, ship8 } };
            var fleet2 = new Fleet(objectId: 11, objectTypeId: 5) { Ships = new[] { ship5, ship6, ship7 } };
            var world = new ModelObject[] { ship1, ship2, ship3, ship4, ship5, ship6, ship7, ship8, fleet1, fleet2 };

            // Create rules & strategies

            var rules = new object[0];
            var actors = new List<ModelObject> { ship1, ship2, ship3, ship4, ship5, ship6, ship7, ship8 };
            var activeActor = actors[0];

            long turnCount = 1;
            int millisecondsInTurn = 10;

            // Circle of life

            while (!IsFightComplete(world))
            {
                Console.Clear();

                var turnStartTime = DateTime.Now;
                Console.WriteLine("==> Turn: {0}. Actor ID: {1}.", turnCount, activeActor.ObjectId);

                PerformTurn(activeActor, world, Attack);

                ShowWorld(world);

                activeActor = NextActor(activeActor, actors);
                turnCount++;

                var turnEndTime = DateTime.Now;
                var turnMilliseconds = (turnEndTime - turnStartTime).TotalMilliseconds;
                var millisecondsToWait = Math.Max(0, millisecondsInTurn - turnMilliseconds);
                Console.WriteLine("Turn took {0} ms. Waiting for {1} to complete the turn.", turnMilliseconds, millisecondsToWait);

                Thread.Sleep((int)millisecondsToWait);
            }
        }

        public static void ShowWorld(ModelObject[] world)
        {
            int worldItemsCount = world.Length;

            Console.WriteLine("World objects:", worldItemsCount);

            for(int i = 0; i < worldItemsCount; i++)
            {
                var worldItem = world[i];
                var worldItemtype = worldItem.GetType();
                if (worldItemtype == typeof(Fleet))
                {
                    ShowFleet(worldItem as Fleet);
                }
                else if (worldItemtype == typeof(Frigate))
                {
                    ShowShip(worldItem as Frigate);
                }
            }
        }

        public static void ShowFleet(Fleet fleet)
        {
            Console.WriteLine("Fleet ID: {0}, Ship ids: {1}.", fleet.ObjectId, string.Join(",", fleet.Ships.Select(ship => ship.ObjectId)));
        }

        public static void ShowShip(Frigate ship)
        {
            Console.WriteLine("Ship ID: {0}, Ship Armor: {1}.", ship.ObjectId, ship.Attributes["Armor"].GetValue());
        }

        public static ModelObject NextActor(ModelObject activeActor, List<ModelObject> actors)
        {
            int startIndex = actors.FindIndex(x => x == activeActor);
            int index = (startIndex + 1) % actors.Count;
            
            while(startIndex != index)
            {
                var ship = actors[index] as Frigate;
                if (ship != null && ship.IsActive())
                {
                    return ship;
                }

                index = (index + 1) % actors.Count;
            }

            return activeActor;
        }

        public static bool IsFightComplete(ModelObject[] world)
        {
            var fleets = world.OfType<Fleet>().ToList();
            return fleets.Any(fleet => fleet.Ships.All(ship => !ship.IsActive()));
        }

        public static void PerformTurn(ModelObject actor, ModelObject[] world, Action<ModelObject, ModelObject> action)
        {
            var fleets = world.OfType<Fleet>().ToList();
            var targetFleet = fleets.FirstOrDefault(x => !x.Ships.Any(ship => ship == actor));
            var targetShip = targetFleet != null ? targetFleet.Ships.FirstOrDefault(ship => ship.IsActive()) : null;
            if (targetShip == null)
            {
                return;
            }

            action(actor, targetShip);
        }

        public static void Attack(ModelObject actor, ModelObject target)
        {
            uint damageGeneated = (uint)(rand.Next(8, 11) * (actor.ObjectTypeId == 2 ? 1.5 : 1.0));
            uint damageApplied = (uint)(damageGeneated * 0.9f);
            var targetFrigate = target as Frigate;            
            targetFrigate.ApplyDamage(damageApplied);
        }
    }

    public class Fleet : ModelObject
    {
        public Fleet(uint objectId, uint objectTypeId)
            : base(objectId, objectTypeId)
        {
        }

        public Frigate[] Ships;
    }

    public class Frigate : AttributedModelObject
    {
        public Frigate(uint objectId, uint objectTypeId, uint armor, float resistance)
            : base(objectId, objectTypeId)
        {
            this.Attributes.Add("Armor", new ModelScalarAttribute(1, 1, "Armor", armor, armor));
            this.Attributes.Add("Resistance", new ModelScalarAttribute(1, 1, "Resistance", 1.0f, resistance / 100.0f));
        }

        public bool IsActive()
        {
            return this.Attributes["Armor"].GetValue() > 0;
        }

        public void ApplyDamage(float value)
        {
            var armorAttribute = this.Attributes["Armor"] as ModelScalarAttribute;
            var resistanceAttrinute = this.Attributes["Resistance"] as ModelScalarAttribute;
            var damageResisted = value * resistanceAttrinute.GetValue();
            var damageApplied = value - damageResisted;
            var armorLeft = Math.Max(armorAttribute.GetValue() - damageApplied, 0.0f);
            armorAttribute.SetValue(armorLeft);
        }
    }
}
