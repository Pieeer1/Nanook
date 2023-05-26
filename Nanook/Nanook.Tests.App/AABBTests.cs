using Nanook.App.Models.Math.Collision;
using Nanook.App.Models.Math;
namespace Nanook.Tests.App
{
    public class AABBTests
    {



        [Fact]
        public void AABBTestHits()
        {
            AABB box1 = new AABB(new Vector2(2, 2), new Vector2(1, 1)); //box to collide with


            AABB box2 = new AABB(new Vector2(3, 3), new Vector2(1, 1));
            Hit? hit1 = box1.IntersectAABB(box2);
            Assert.NotNull(hit1);
            Assert.Equal(3, hit1.Position.X);
            Assert.Equal(1, hit1.Position.Y);

            Hit? hit2 = box2.IntersectAABB(box1);
            Assert.NotNull(hit2);
            Assert.Equal(2, hit2.Position.X);
            Assert.Equal(4f, hit2.Position.Y);

            AABB box3 = new AABB(new Vector2(3.999f, 3.999f), new Vector2(1, 1));
            Hit? hit3 = box3.IntersectAABB(box1);
            Assert.NotNull(hit3);
            Assert.Equal(2, hit3.Position.X);
            Assert.Equal(4.99900007f, hit3.Position.Y);

            Hit? hit4 = box1.IntersectAABB(box3);
            Assert.NotNull(hit4);
            Assert.Equal(3.99900007f, hit4.Position.X);
            Assert.Equal(1f, hit4.Position.Y);

            AABB box4 = new AABB(new Vector2(1, 1), new Vector2(1, 1));
            Hit? hit5 = box4.IntersectAABB(box1);
            Assert.NotNull(hit5);
            Assert.Equal(2, hit5.Position.X);
            Assert.Equal(0, hit5.Position.Y);
            Assert.Equal(0, hit5.Delta.X);
            Assert.Equal(1, hit5.Delta.Y);

            Hit? hit6 = box1.IntersectAABB(box4);
            Assert.NotNull(hit6);
            Assert.Equal(1, hit6.Position.X);
            Assert.Equal(3, hit6.Position.Y);
            Assert.Equal(0, hit6.Delta.X);
            Assert.Equal(-1, hit6.Delta.Y);
        }
        [Fact]
        public void AABBTestNonHits()
        {
            AABB box1 = new AABB(new Vector2(2, 2), new Vector2(1, 1)); //box to collide with

            AABB box2 = new AABB(new Vector2(4, 4), new Vector2(1, 1));
            Assert.Null(box1.IntersectAABB(box2));
            Assert.Null(box2.IntersectAABB(box1));

            AABB box3 = new AABB(new Vector2(0, 0), new Vector2(1, 1));
            Assert.Null(box1.IntersectAABB(box3));
            Assert.Null(box3.IntersectAABB(box1));

            AABB box4 = new AABB(new Vector2(0, 4), new Vector2(1, 1));
            Assert.Null(box1.IntersectAABB(box4));
            Assert.Null(box4.IntersectAABB(box1));

            AABB box5 = new AABB(new Vector2(4, 0), new Vector2(1, 1));
            Assert.Null(box1.IntersectAABB(box5));
            Assert.Null(box5.IntersectAABB(box1));
        }
    }
}