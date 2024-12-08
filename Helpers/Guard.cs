using Helpers;

namespace Day6Lib;

public class Guard(Point startingPos)
{
   private enum EDirection { Up, Right, Down, Left };

   public bool StuckInLoop { get; private set; }
   private EDirection Facing { get; set; } = EDirection.Up;
   private Point Position { get; set; } = startingPos;
   public HashSet<Point> WalkedSpaces { get; } = [];

   private readonly HashSet<Tuple<Point, EDirection>> _loopPoints = [];

   private void Move()
   {
      WalkedSpaces.Add(Position);
      Position = GetNextSpace();
   }

   private Point GetNextSpace()
   {
      return Position + Facing switch
                        {
                           EDirection.Up => new Point(0, -1),
                           EDirection.Right => new Point(1, 0),
                           EDirection.Down => new Point(0, 1),
                           EDirection.Left => new Point(-1, 0),
                           _ => throw new ArgumentOutOfRangeException(nameof(Facing), Facing, null)
                        };
   }

   private bool CheckLoop()
   {
      return _loopPoints.Any(p => Equals(p, new Tuple<Point, EDirection>(Position, Facing)));
   }

   private void TurnRight()
   {
      _loopPoints.Add(new Tuple<Point, EDirection>(Position, Facing));
      Facing = Facing switch
               {
                  EDirection.Up => EDirection.Right,
                  EDirection.Right => EDirection.Down,
                  EDirection.Down => EDirection.Left,
                  EDirection.Left => EDirection.Up,
                  _ => throw new ArgumentOutOfRangeException(nameof(Facing), Facing, null)
               };
   }

   public void WalkMap(List<List<char>> map)
   {
      var nextSpace = GetNextSpace();
      do
      {
         if (map[nextSpace.Y][nextSpace.X] == '#' || map[nextSpace.Y][nextSpace.X] == 'O')
         {
            if (CheckLoop())
            {
               StuckInLoop = true;
               break;
            }

            TurnRight();
         }
         else
         {
            Move();
         }

         nextSpace = GetNextSpace();
      } while (nextSpace.X > 0 && nextSpace.X < map[0].Count &&
               nextSpace.Y > 0 && nextSpace.Y < map.Count);

      WalkedSpaces.Add(Position);
   }
}
