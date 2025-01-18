using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prac2
{
    //nodes for the search tree
    //we dont need to remember siblings(so its not really a tree just a linked list)
    //because a sibling is the same vakje but trying out the next value of its domain
    //so we dont need to remember the coordinates again
    internal class MCV_Node
    {
        //the coordinates of the vakje that we filled in
        public (int, int) coordinates;
        //the previous node
        private MCV_Node prev;
        //the next node
        private MCV_Node next;

        public MCV_Node(Vakje vakje)
        {
            coordinates = vakje.coordinates;
        }

        public void setChild(MCV_Node child)
        {
            next = child;
            child.prev = this;
        }

        public void deleteChild()
        {
            next = null;
        }

        public MCV_Node getParent()
        {
            return prev;
        }
    }
}
