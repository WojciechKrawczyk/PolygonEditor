using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolygonEditor
{
    public partial class Form1 : Form
    {
        public DrawingManagement drawingManagement = new DrawingManagement();
        private List<MyPolygon> myPolygons = new List<MyPolygon>();

        int indexOfEditingPolygon;
        int indexOfEditingVertex;
        int indexOfEditingLineSegment;

        //0 - library, 1 - Bresenham, 2 - Wu
        int typeOfDrawingAlgoritm = 0;

        bool canDraw = true;

        bool movingVertex = false;
        bool movingLineSegment = true;
        bool movingPolygon = false;
        Point startMovingLineSegment;
        Point startMovingPolygon;
        Point p1, p2;

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            GenerateStartPolygon();
        }

        public bool SearchVertex(MouseEventArgs e)
        {
            for (int i = 0; i < myPolygons.Count; i++)
            {
                for (int j = 0; j < myPolygons[i].points.Count; j++)
                {
                    if (DrawingManagement.PointsDistance(myPolygons[i].points[j], e.Location) < 5)
                    {
                        indexOfEditingPolygon = i;
                        indexOfEditingVertex = j;
                        //move vertex
                        if (e.Button == MouseButtons.Left)
                        {
                            movingVertex = true;
                            return true;
                        }
                        //delete vertex
                        if (e.Button == MouseButtons.Right)
                        {
                            canDraw = false;
                            pictureBox.ContextMenuStrip = contextMenuVertex;
                            contextMenuVertex.Show(e.Location);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool SearchLineSegment(MouseEventArgs e)
        {
            for (int i = 0; i < myPolygons.Count; i++)
            {
                for (int j = 0; j < myPolygons[i].lineSegments.Count; j++)
                {
                    int l = DrawingManagement.PointsDistance(myPolygons[i].lineSegments[j].p1, myPolygons[i].lineSegments[j].p2);
                    int l1 = DrawingManagement.PointsDistance(myPolygons[i].lineSegments[j].p1, e.Location);
                    int l2 = DrawingManagement.PointsDistance(myPolygons[i].lineSegments[j].p2, e.Location);
                    if (Math.Abs(l - l1 - l2) < 10)
                    {
                        indexOfEditingPolygon = i;
                        indexOfEditingLineSegment = j;
                        //add vertex to line segmnet in the center
                        if (e.Button == MouseButtons.Right)
                        {
                            canDraw = false;
                            pictureBox.ContextMenuStrip = contextMenuLineSegment;
                            contextMenuLineSegment.Show(e.Location);
                            if (myPolygons[indexOfEditingPolygon].lineSegments[indexOfEditingLineSegment].HasRestriction())
                            {
                                contextMenuLineSegment.Items[1].Enabled = false;
                                contextMenuLineSegment.Items[2].Enabled = false;
                                contextMenuLineSegment.Items[3].Enabled = false;
                                contextMenuLineSegment.Items[4].Enabled = true;
                            }
                            else
                            {
                                contextMenuLineSegment.Items[1].Enabled = true;
                                contextMenuLineSegment.Items[2].Enabled = true;
                                contextMenuLineSegment.Items[3].Enabled = true;
                                contextMenuLineSegment.Items[4].Enabled = false;
                            }
                            return true;
                        }
                        //move line segment
                        if (e.Button == MouseButtons.Left)
                        {
                            movingLineSegment = true;
                            startMovingLineSegment = e.Location;
                            p1 = myPolygons[indexOfEditingPolygon].lineSegments[indexOfEditingLineSegment].p1;
                            p2 = myPolygons[indexOfEditingPolygon].lineSegments[indexOfEditingLineSegment].p2;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool SearchPolygon(MouseEventArgs e)
        {
            //searching for polygon for editing
            for (int i = 0; i < myPolygons.Count; i++)
            {
                if (IsPointInPolygon(e.Location, myPolygons[i].points.ToArray()))
                {
                    indexOfEditingPolygon = i;

                    //move polygon
                    if (e.Button == MouseButtons.Left)
                    {
                        movingPolygon = true;
                        startMovingPolygon = e.Location;
                        return true;
                    }
                    //delete polygon
                    if (e.Button == MouseButtons.Right)
                    {
                        pictureBox.ContextMenuStrip = contextMenuPolygon;
                        canDraw = false;
                        contextMenuPolygon.Show(e.Location);
                        return true;
                    }
                }
            }
            return false;
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox.ContextMenuStrip = null;
            bool t = false;
            if (!drawingManagement.active)
            {
                //searching vertex for editing
                t = SearchVertex(e);

                //searching line segments for editing
                if (!t) 
                    t = SearchLineSegment(e);

                //searching for polygon for editing
                if (!t) 
                    t = SearchPolygon(e);
            }

            if (t)
                return;

            if (!canDraw)
                canDraw = true;

            //drawing polygon
            else if (e.Button == MouseButtons.Left)
            {
                drawingManagement.ConsiderVertex(e.Location, picBoxVer, picBoxHor, picBoxLen);
                if (drawingManagement.isEnd)
                {
                    myPolygons.Add(new MyPolygon(drawingManagement.lineSegments));
                    drawingManagement.Reset();
                }
                pictureBox.Invalidate();
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawingManagement.active)
            {
                drawingManagement.Moving(e.Location);
                pictureBox.Invalidate();
                return;
            }
            if (movingVertex)
            {
                if (e.Button == MouseButtons.Left)
                {
                    //Code without line restricion
                    //myPolygons[indexOfEditingPolygon].points[indexOfEditingVertex] = e.Location;
                    //myPolygons[indexOfEditingPolygon].lineSegments[indexOfEditingVertex].p1 = e.Location;
                    //int k;
                    //if (indexOfEditingVertex == 0)
                    //    k = myPolygons[indexOfEditingPolygon].points.Count - 1;
                    //else
                    //    k = indexOfEditingVertex - 1;
                    //myPolygons[indexOfEditingPolygon].lineSegments[k].p2 = e.Location;
                    //pictureBox.Invalidate();
                    //return;

                    Point p = new Point(myPolygons[indexOfEditingPolygon].points[indexOfEditingVertex].X,
                        myPolygons[indexOfEditingPolygon].points[indexOfEditingVertex].Y);
                    myPolygons[indexOfEditingPolygon].points[indexOfEditingVertex] = e.Location;

                    int xChange = e.Location.X - p.X;
                    int yChange = e.Location.Y - p.Y;

                    int nextLineIndex = indexOfEditingVertex;
                    int nextVertexIndex = CalculateNextIndexOfVertex(indexOfEditingVertex);
                    int restriction = 0;

                    while (myPolygons[indexOfEditingPolygon].lineSegments[nextLineIndex].HasRestriction())
                    {
                        if (myPolygons[indexOfEditingPolygon].lineSegments[nextLineIndex].lengthRestriction)
                        {
                            int x = myPolygons[indexOfEditingPolygon].points[nextVertexIndex].X + xChange;
                            int y = myPolygons[indexOfEditingPolygon].points[nextVertexIndex].Y + yChange;
                            myPolygons[indexOfEditingPolygon].points[nextVertexIndex] = new Point(x, y);
                            restriction = 0;
                        }
                        else if (myPolygons[indexOfEditingPolygon].lineSegments[nextLineIndex].horizontalRestriction)
                        {
                            int x = myPolygons[indexOfEditingPolygon].points[nextVertexIndex].X;
                            int k = CalculatePreviousIndexOfVertex(nextVertexIndex);
                            int y = myPolygons[indexOfEditingPolygon].points[k].Y;
                            xChange = 0;
                            yChange = y - myPolygons[indexOfEditingPolygon].points[nextVertexIndex].Y;
                            myPolygons[indexOfEditingPolygon].points[nextVertexIndex] = new Point(x, y);
                            if (restriction == 2)
                                break;
                            else
                                restriction = 1;
                        }
                        else if (myPolygons[indexOfEditingPolygon].lineSegments[nextLineIndex].verticalRestriction)
                        {
                            int k = CalculatePreviousIndexOfVertex(nextVertexIndex);
                            int x = myPolygons[indexOfEditingPolygon].points[k].X;
                            int y = myPolygons[indexOfEditingPolygon].points[nextVertexIndex].Y;
                            yChange = 0;
                            xChange = x - myPolygons[indexOfEditingPolygon].points[nextVertexIndex].X;
                            myPolygons[indexOfEditingPolygon].points[nextVertexIndex] = new Point(x, y);
                            if (restriction == 1)
                                break;
                            else
                                restriction = 2;
                        }
                        nextLineIndex = nextVertexIndex;
                        nextVertexIndex = CalculateNextIndexOfVertex(nextVertexIndex);
                    }

                    int previousLineIndex = CalculateIndexOfPreviousLineSegmentFromPoint(indexOfEditingVertex);
                    int previousVertexIndex = CalculatePreviousIndexOfVertex(indexOfEditingVertex);
                    restriction = 0;

                    while (myPolygons[indexOfEditingPolygon].lineSegments[previousLineIndex].HasRestriction())
                    {
                        if (myPolygons[indexOfEditingPolygon].lineSegments[previousLineIndex].lengthRestriction)
                        {
                            int x = myPolygons[indexOfEditingPolygon].points[previousVertexIndex].X + xChange;
                            int y = myPolygons[indexOfEditingPolygon].points[previousVertexIndex].Y + yChange;
                            myPolygons[indexOfEditingPolygon].points[previousVertexIndex] = new Point(x, y);
                            restriction = 0;
                        }
                        else if (myPolygons[indexOfEditingPolygon].lineSegments[previousLineIndex].horizontalRestriction)
                        {
                            int x = myPolygons[indexOfEditingPolygon].points[previousVertexIndex].X;
                            int k = CalculateNextIndexOfVertex(previousVertexIndex);
                            int y = myPolygons[indexOfEditingPolygon].points[k].Y;
                            xChange = 0;
                            yChange = y - myPolygons[indexOfEditingPolygon].points[nextVertexIndex].Y;
                            myPolygons[indexOfEditingPolygon].points[previousVertexIndex] = new Point(x, y);
                            if (restriction == 2)
                                break;
                            else
                                restriction = 1;
                        }
                        else if (myPolygons[indexOfEditingPolygon].lineSegments[previousLineIndex].verticalRestriction)
                        {
                            int k = CalculateNextIndexOfVertex(previousVertexIndex);
                            int x = myPolygons[indexOfEditingPolygon].points[k].X;
                            int y = myPolygons[indexOfEditingPolygon].points[previousVertexIndex].Y;
                            yChange = 0;
                            xChange = x - myPolygons[indexOfEditingPolygon].points[nextVertexIndex].X;
                            myPolygons[indexOfEditingPolygon].points[previousVertexIndex] = new Point(x, y);
                            if (restriction == 1)
                                break;
                            else
                                restriction = 2;
                        }
                        previousVertexIndex = CalculatePreviousIndexOfVertex(previousVertexIndex);
                        previousLineIndex = previousVertexIndex;
                    }

                    List<LineSegment> lineSegments = GenerateLinesegmentsFromPoints(myPolygons[indexOfEditingPolygon].points);
                    lineSegments = ReassignLineRestriction(lineSegments);
                    myPolygons[indexOfEditingPolygon] = new MyPolygon(lineSegments);

                    pictureBox.Invalidate();
                    return;
                }
            }
            if (movingLineSegment)
            {
                if (e.Button == MouseButtons.Left)
                {
                    int xChange = e.Location.X - startMovingLineSegment.X;
                    int yChange = e.Location.Y - startMovingLineSegment.Y;
                    int x1 = p1.X + xChange;
                    int y1 = p1.Y + yChange;
                    int x2 = p2.X + xChange;
                    int y2 = p2.Y + yChange;
                    int b;
                    if (indexOfEditingLineSegment == myPolygons[indexOfEditingPolygon].lineSegments.Count - 1)
                        b = 0;
                    else
                        b = indexOfEditingLineSegment + 1;
                    myPolygons[indexOfEditingPolygon].points[indexOfEditingLineSegment] = new Point(x1, y1);
                    myPolygons[indexOfEditingPolygon].points[b] = new Point(x2, y2);

                    (int nextLineIndex, int previousLineIndex) = CalculatePreviousAndNextIndexOfLineSegment();
                    int nextVertexIndex = nextLineIndex;
                    int restriction = 0;

                    while (myPolygons[indexOfEditingPolygon].lineSegments[nextLineIndex].HasRestriction())
                    {
                        if (myPolygons[indexOfEditingPolygon].lineSegments[nextLineIndex].lengthRestriction)
                        {
                            int x = myPolygons[indexOfEditingPolygon].points[nextVertexIndex].X + xChange;
                            int y = myPolygons[indexOfEditingPolygon].points[nextVertexIndex].Y + yChange;
                            myPolygons[indexOfEditingPolygon].points[nextVertexIndex] = new Point(x, y);
                            restriction = 0;
                        }
                        else if (myPolygons[indexOfEditingPolygon].lineSegments[nextLineIndex].horizontalRestriction)
                        {
                            int x = myPolygons[indexOfEditingPolygon].points[nextVertexIndex].X;
                            int k = CalculatePreviousIndexOfVertex(nextVertexIndex);
                            int y = myPolygons[indexOfEditingPolygon].points[k].Y;
                            xChange = 0;
                            yChange = y - myPolygons[indexOfEditingPolygon].points[nextVertexIndex].Y;
                            myPolygons[indexOfEditingPolygon].points[nextVertexIndex] = new Point(x, y);
                            if (restriction == 2)
                                break;
                            else
                                restriction = 1;
                        }
                        else if (myPolygons[indexOfEditingPolygon].lineSegments[nextLineIndex].verticalRestriction)
                        {
                            int k = CalculatePreviousIndexOfVertex(nextVertexIndex);
                            int x = myPolygons[indexOfEditingPolygon].points[k].X;
                            int y = myPolygons[indexOfEditingPolygon].points[nextVertexIndex].Y;
                            yChange = 0;
                            xChange = x - myPolygons[indexOfEditingPolygon].points[nextVertexIndex].X;
                            myPolygons[indexOfEditingPolygon].points[nextVertexIndex] = new Point(x, y);
                            if (restriction == 1)
                                break;
                            else
                                restriction = 2;
                        }
                        nextLineIndex = nextVertexIndex;
                        nextVertexIndex = CalculateNextIndexOfVertex(nextVertexIndex);
                    }

                    int previousVertexIndex = previousLineIndex;
                    restriction = 0;

                    while (myPolygons[indexOfEditingPolygon].lineSegments[previousLineIndex].HasRestriction())
                    {
                        if (myPolygons[indexOfEditingPolygon].lineSegments[previousLineIndex].lengthRestriction)
                        {
                            int x = myPolygons[indexOfEditingPolygon].points[previousVertexIndex].X + xChange;
                            int y = myPolygons[indexOfEditingPolygon].points[previousVertexIndex].Y + yChange;
                            myPolygons[indexOfEditingPolygon].points[previousVertexIndex] = new Point(x, y);
                            restriction = 0;
                        }
                        else if (myPolygons[indexOfEditingPolygon].lineSegments[previousLineIndex].horizontalRestriction)
                        {
                            int x = myPolygons[indexOfEditingPolygon].points[previousVertexIndex].X;
                            int k = CalculateNextIndexOfVertex(previousVertexIndex);
                            int y = myPolygons[indexOfEditingPolygon].points[k].Y;
                            xChange = 0;
                            yChange = y - myPolygons[indexOfEditingPolygon].points[nextVertexIndex].Y;
                            myPolygons[indexOfEditingPolygon].points[previousVertexIndex] = new Point(x, y);
                            if (restriction == 2)
                                break;
                            else
                                restriction = 1;
                        }
                        else if (myPolygons[indexOfEditingPolygon].lineSegments[previousLineIndex].verticalRestriction)
                        {
                            int k = CalculateNextIndexOfVertex(previousVertexIndex);
                            int x = myPolygons[indexOfEditingPolygon].points[k].X;
                            int y = myPolygons[indexOfEditingPolygon].points[previousVertexIndex].Y;
                            yChange = 0;
                            xChange = x - myPolygons[indexOfEditingPolygon].points[nextVertexIndex].X;
                            myPolygons[indexOfEditingPolygon].points[previousVertexIndex] = new Point(x, y);
                            if (restriction == 1)
                                break;
                            else
                                restriction = 2;
                        }
                        previousVertexIndex = CalculatePreviousIndexOfVertex(previousVertexIndex);
                        previousLineIndex = previousVertexIndex;
                    }

                    List<LineSegment> lineSegments = GenerateLinesegmentsFromPoints(myPolygons[indexOfEditingPolygon].points);
                    lineSegments = ReassignLineRestriction(lineSegments);
                    myPolygons[indexOfEditingPolygon] = new MyPolygon(lineSegments);
                    pictureBox.Invalidate();
                    return;
                }
            }
            if (movingPolygon)
            {
                if (e.Button == MouseButtons.Left)
                {
                    int xChange = e.Location.X - startMovingPolygon.X;
                    int yChange = e.Location.Y - startMovingPolygon.Y;
                    List<Point> points = myPolygons[indexOfEditingPolygon].points;
                    for (int i = 0; i < points.Count; i++)
                    {
                        points[i] = new Point(points[i].X + xChange, points[i].Y + yChange);
                    }
                    List<LineSegment> lineSegments = GenerateLinesegmentsFromPoints(myPolygons[indexOfEditingPolygon].points);
                    lineSegments = ReassignLineRestriction(lineSegments);
                    myPolygons[indexOfEditingPolygon] = new MyPolygon(lineSegments);
                    startMovingPolygon = e.Location;
                    pictureBox.Invalidate();
                    return;
                }
            }
        }

        public List<LineSegment> ReassignLineRestriction(List<LineSegment> lineSegments)
        {
            for (int i = 0; i < lineSegments.Count; i++)
            {
                if (myPolygons[indexOfEditingPolygon].lineSegments[i].verticalRestriction)
                {
                    lineSegments[i].AssignVerticalHestriction();
                }
                else if (myPolygons[indexOfEditingPolygon].lineSegments[i].horizontalRestriction)
                {
                    lineSegments[i].AssignHorizontalHestriction();
                }
                else if (myPolygons[indexOfEditingPolygon].lineSegments[i].lengthRestriction)
                {
                    lineSegments[i].AssignLengthHestriction();
                }
            }
            return lineSegments;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            drawingManagement.Draw(g);
            foreach (var p in myPolygons)
                p.Draw(g, Pens.Black, typeOfDrawingAlgoritm);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C)
            {
                drawingManagement.Reset();
                pictureBox.Invalidate();
            }
        }

        private void deleteVertexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myPolygons[indexOfEditingPolygon].points.Count == 3)
            {
                myPolygons.RemoveAt(indexOfEditingPolygon);
                MessageBox.Show("Usuwasz wierzchołek trójkata. \nPowoduje to jego usunięcie, " +
                    "ponieważ rysowane sa tylko wielokąty. \nOdcinki nie są obsługiwane.");
                pictureBox.Invalidate();
                return;
            }
            myPolygons[indexOfEditingPolygon].points.RemoveAt(indexOfEditingVertex);
            List<LineSegment> lineSegments = GenerateLinesegmentsFromPoints(myPolygons[indexOfEditingPolygon].points);

            //editing first or last vertex
            if(indexOfEditingVertex == 0 || indexOfEditingVertex == myPolygons[indexOfEditingPolygon].points.Count)
            {
                int k = 0;
                if (indexOfEditingVertex == 0)
                    k = 1;
                for (int i = 0; i < lineSegments.Count - 1; i++)
                {
                    if (myPolygons[indexOfEditingPolygon].lineSegments[i + k].verticalRestriction)
                    {
                        lineSegments[i].AssignVerticalHestriction();
                    }
                    else if (myPolygons[indexOfEditingPolygon].lineSegments[i + k].horizontalRestriction)
                    {
                        lineSegments[i].AssignHorizontalHestriction();
                    }
                    else if (myPolygons[indexOfEditingPolygon].lineSegments[i + k].lengthRestriction)
                    {
                        lineSegments[i].AssignLengthHestriction();
                    }
                }
            } 
            else
            {
                //before editing line segment
                for (int i = 0; i < indexOfEditingVertex - 1; i++)
                {
                    if (myPolygons[indexOfEditingPolygon].lineSegments[i].verticalRestriction)
                    {
                        lineSegments[i].AssignVerticalHestriction();
                    }
                    else if (myPolygons[indexOfEditingPolygon].lineSegments[i].horizontalRestriction)
                    {
                        lineSegments[i].AssignHorizontalHestriction();
                    }
                    else if (myPolygons[indexOfEditingPolygon].lineSegments[i].lengthRestriction)
                    {
                        lineSegments[i].AssignLengthHestriction();
                    }
                }

                //after editing line segment
                for (int i = indexOfEditingVertex; i < lineSegments.Count; i++)
                {
                    if (myPolygons[indexOfEditingPolygon].lineSegments[i + 1].verticalRestriction) 
                    {
                        lineSegments[i].AssignVerticalHestriction();
                    }
                    else if (myPolygons[indexOfEditingPolygon].lineSegments[i + 1].horizontalRestriction)
                    {
                        lineSegments[i].AssignHorizontalHestriction();
                    }
                    else if (myPolygons[indexOfEditingPolygon].lineSegments[i + 1].lengthRestriction)
                    {
                        lineSegments[i].AssignLengthHestriction();
                    }
                }
            }

            myPolygons[indexOfEditingPolygon] = new MyPolygon(lineSegments);
            pictureBox.ContextMenuStrip = null;
            canDraw = true;
            pictureBox.Invalidate();
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            movingVertex = false;
            movingLineSegment = false;
            movingPolygon = false;
        }

        private void addVertexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point p1 = myPolygons[indexOfEditingPolygon].lineSegments[indexOfEditingLineSegment].p1;
            Point p2 = myPolygons[indexOfEditingPolygon].lineSegments[indexOfEditingLineSegment].p2;
            Point x = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
            myPolygons[indexOfEditingPolygon].points.Insert(indexOfEditingLineSegment + 1, x);
            List<LineSegment> lineSegments = GenerateLinesegmentsFromPoints(myPolygons[indexOfEditingPolygon].points);

            //before editing line segment
            for (int i = 0; i < indexOfEditingLineSegment; i++)
            {
                if (myPolygons[indexOfEditingPolygon].lineSegments[i].verticalRestriction)
                {
                    lineSegments[i].AssignVerticalHestriction();
                }
                else if (myPolygons[indexOfEditingPolygon].lineSegments[i].horizontalRestriction)
                {
                    lineSegments[i].AssignHorizontalHestriction();
                }
                else if (myPolygons[indexOfEditingPolygon].lineSegments[i].lengthRestriction)
                {
                    lineSegments[i].AssignLengthHestriction();
                }
            }

            //after editing line segment
            for (int i = indexOfEditingLineSegment + 2; i < lineSegments.Count; i++)
            {
                if (myPolygons[indexOfEditingPolygon].lineSegments[i - 1].verticalRestriction)
                {
                    lineSegments[i].AssignVerticalHestriction();
                }
                else if (myPolygons[indexOfEditingPolygon].lineSegments[i - 1].horizontalRestriction)
                {
                    lineSegments[i].AssignHorizontalHestriction();
                }
                else if (myPolygons[indexOfEditingPolygon].lineSegments[i - 1].lengthRestriction)
                {
                    lineSegments[i].AssignLengthHestriction();
                }
            }

            myPolygons[indexOfEditingPolygon] = new MyPolygon(lineSegments);
            pictureBox.ContextMenuStrip = null;
            canDraw = true;
            pictureBox.Invalidate();
        }

        public List<LineSegment> GenerateLinesegmentsFromPoints(List<Point> points)
        {
            List<LineSegment> newLineSegments = new List<LineSegment>();
            for (int i = 0; i < points.Count - 1; i++)
            {
                newLineSegments.Add(new LineSegment(points[i], points[i + 1], picBoxVer.Image, picBoxHor.Image, picBoxLen.Image));
            }
            newLineSegments.Add(new LineSegment(points[points.Count - 1], points[0], picBoxVer.Image, picBoxHor.Image, picBoxLen.Image));
            return newLineSegments;
        }

        private void deletePolygonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myPolygons.RemoveAt(indexOfEditingPolygon);
            pictureBox.ContextMenuStrip = null;
            canDraw = true;
            pictureBox.Invalidate();
        }

        public bool IsPointInPolygon(Point p, Point[] polygon)
        {
            double minX = polygon[0].X;
            double maxX = polygon[0].X;
            double minY = polygon[0].Y;
            double maxY = polygon[0].Y;
            for (int i = 1; i < polygon.Length; i++)
            {
                Point q = polygon[i];
                minX = Math.Min(q.X, minX);
                maxX = Math.Max(q.X, maxX);
                minY = Math.Min(q.Y, minY);
                maxY = Math.Max(q.Y, maxY);
            }

            if (p.X < minX || p.X > maxX || p.Y < minY || p.Y > maxY)
            {
                return false;
            }

            bool inside = false;
            for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
            {
                if ((polygon[i].Y > p.Y) != (polygon[j].Y > p.Y) &&
                     p.X < (polygon[j].X - polygon[i].X) * (p.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X)
                {
                    inside = !inside;
                }
            }

            return inside;
        }

        private void horizontalRestrictionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (int previous, int next) = CalculatePreviousAndNextIndexOfLineSegment();

            if(myPolygons[indexOfEditingPolygon].lineSegments[previous].horizontalRestriction 
                || myPolygons[indexOfEditingPolygon].lineSegments[next].horizontalRestriction)
            {
                MessageBox.Show("Dwie sąsiadujące krawędzie nie mogą mieć poziomego ograniczenia!");
                return;
            }

            bool canModify = false;

            if (!myPolygons[indexOfEditingPolygon].lineSegments[previous].lengthRestriction
                || !myPolygons[indexOfEditingPolygon].lineSegments[next].lengthRestriction)
                canModify = true;

            else if (myPolygons[indexOfEditingPolygon].lineSegments[previous].lengthRestriction
                && myPolygons[indexOfEditingPolygon].lineSegments[next].lengthRestriction)
            {
                for (int i = 0; i < myPolygons[indexOfEditingPolygon].lineSegments.Count; i++)
                {
                    if (i != indexOfEditingLineSegment && !myPolygons[indexOfEditingPolygon].lineSegments[i].HasRestriction())
                    {
                        canModify = true;
                        break;
                    }
                }
            }

            if (!canModify)
            {
                MessageBox.Show("Twój wielokąt ma już zbyt wiele ograniczeń. " +
                    "\nAby ograniczyć w poziomie daną krawędź: " +
                    "\n1. Usuń na przynajmniej jednej przyległej krawędzi ograniczenie na długość." +
                    "\n2. Dodaj poziome ograniczenie na żądaną krawędź." +
                    "\n3. Popraw ustawinie krwędzi, z której usunąłeś ograniczenie na długość." +
                    "\n4. Dodaj na poprawioną krawędź ograniczenie na długość.");
                return;
            }

            myPolygons[indexOfEditingPolygon].lineSegments[indexOfEditingLineSegment].AssignHorizontalHestriction();

            //int k = indexOfEditingLineSegment + 1;
            //if (indexOfEditingLineSegment == myPolygons[indexOfEditingPolygon].lineSegments.Count - 1)
            //    k = 0;
            //LineSegment line = myPolygons[indexOfEditingPolygon].lineSegments[indexOfEditingLineSegment];
            //Point point = new Point();
            //point.X = line.p2.X;
            //point.Y = line.p1.Y;
            //myPolygons[indexOfEditingPolygon].points[k] = point;

            LineSegment line = myPolygons[indexOfEditingPolygon].lineSegments[indexOfEditingLineSegment];
            Point point = new Point();

            if (!myPolygons[indexOfEditingPolygon].lineSegments[next].lengthRestriction)
            {
                point.X = line.p2.X;
                point.Y = line.p1.Y;
                myPolygons[indexOfEditingPolygon].points[next] = point;
            }
            else if (!myPolygons[indexOfEditingPolygon].lineSegments[previous].lengthRestriction)
            {
                point.X = line.p1.X;
                point.Y = line.p2.Y;
                myPolygons[indexOfEditingPolygon].points[indexOfEditingLineSegment] = point;
            }
            else
            {
                point.X = line.p2.X;
                point.Y = line.p1.Y;
                int xChange = point.X - line.p2.X;
                int yChange = point.Y - line.p2.Y;

                Point p = new Point();
                while (myPolygons[indexOfEditingPolygon].lineSegments[next].HasRestriction())
                {
                    p.X = myPolygons[indexOfEditingPolygon].points[next].X + xChange;
                    p.Y = myPolygons[indexOfEditingPolygon].points[next].Y + yChange;
                    myPolygons[indexOfEditingPolygon].points[next] = new Point(p.X, p.Y);

                    indexOfEditingLineSegment = next;
                    (previous, next) = CalculatePreviousAndNextIndexOfLineSegment();
                }
                p.X = myPolygons[indexOfEditingPolygon].points[next].X + xChange;
                p.Y = myPolygons[indexOfEditingPolygon].points[next].Y + yChange;
                myPolygons[indexOfEditingPolygon].points[next] = new Point(p.X, p.Y);
            }

            List<LineSegment> lineSegments = GenerateLinesegmentsFromPoints(myPolygons[indexOfEditingPolygon].points);
            for (int i = 0; i < lineSegments.Count; i++)
            {
                if (myPolygons[indexOfEditingPolygon].lineSegments[i].verticalRestriction)
                {
                    lineSegments[i].AssignVerticalHestriction();
                }
                else if (myPolygons[indexOfEditingPolygon].lineSegments[i].horizontalRestriction)
                {
                    lineSegments[i].AssignHorizontalHestriction();
                }
                else if (myPolygons[indexOfEditingPolygon].lineSegments[i].lengthRestriction)
                {
                    lineSegments[i].AssignLengthHestriction();
                }
            }
            myPolygons[indexOfEditingPolygon] = new MyPolygon(lineSegments);

            pictureBox.Invalidate();
        }

        private void lengthRestricionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myPolygons[indexOfEditingPolygon].lineSegments[indexOfEditingLineSegment].AssignLengthHestriction();
            pictureBox.Invalidate();
        }

        private void deleteRestrictionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myPolygons[indexOfEditingPolygon].lineSegments[indexOfEditingLineSegment].ResetResriction();
            pictureBox.Invalidate();
        }

        private void verticalRestrictionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (int previous, int next) = CalculatePreviousAndNextIndexOfLineSegment();

            if (myPolygons[indexOfEditingPolygon].lineSegments[previous].verticalRestriction
                || myPolygons[indexOfEditingPolygon].lineSegments[next].verticalRestriction)
            {
                MessageBox.Show("Dwie sąsiadujące krawędzie nie mogą mieć pionowego ograniczenia!");
                return;
            }

            bool canModify = false;

            if (!myPolygons[indexOfEditingPolygon].lineSegments[previous].lengthRestriction
                || !myPolygons[indexOfEditingPolygon].lineSegments[next].lengthRestriction)
                canModify = true;

            else if (myPolygons[indexOfEditingPolygon].lineSegments[previous].lengthRestriction 
                && myPolygons[indexOfEditingPolygon].lineSegments[next].lengthRestriction)
            {
                for (int i = 0; i < myPolygons[indexOfEditingPolygon].lineSegments.Count; i++)
                {
                    if(i != indexOfEditingLineSegment && !myPolygons[indexOfEditingPolygon].lineSegments[i].HasRestriction())
                    {
                        canModify = true;
                        break;
                    }
                }
            }

            if (!canModify)
            {
                MessageBox.Show("Twój wielokąt ma już zbyt wiele ograniczeń. " +
                    "\nAby ograniczyć w pionie daną krawędź: " +
                    "\n1. Usuń na przynajmniej jednej przyległej krawędzi ograniczenie na długość." +
                    "\n2. Dodaj pionowe ograniczenie na żądaną krawędź." +
                    "\n3. Popraw ustawinie krwędzi, z której usunąłeś ograniczenie na długość." +
                    "\n4. Dodaj na poprawioną krawędź ograniczenie na długość.");
                return;
            }

            myPolygons[indexOfEditingPolygon].lineSegments[indexOfEditingLineSegment].AssignVerticalHestriction();

            LineSegment line = myPolygons[indexOfEditingPolygon].lineSegments[indexOfEditingLineSegment];
            Point point = new Point();

            if (!myPolygons[indexOfEditingPolygon].lineSegments[next].lengthRestriction)
            {
                point.X = line.p1.X;
                point.Y = line.p2.Y;
                myPolygons[indexOfEditingPolygon].points[next] = point;
            }
            else if (!myPolygons[indexOfEditingPolygon].lineSegments[previous].lengthRestriction)
            {
                point.X = line.p2.X;
                point.Y = line.p1.Y;
                myPolygons[indexOfEditingPolygon].points[indexOfEditingLineSegment] = point;
            }
            else
            {
                point.X = line.p1.X;
                point.Y = line.p2.Y;
                int xChange = point.X - line.p2.X;
                int yChange = point.Y - line.p2.Y;
        
                Point p = new Point();
                while (myPolygons[indexOfEditingPolygon].lineSegments[next].HasRestriction())
                {
                    p.X = myPolygons[indexOfEditingPolygon].points[next].X + xChange;
                    p.Y = myPolygons[indexOfEditingPolygon].points[next].Y + yChange;
                    myPolygons[indexOfEditingPolygon].points[next] = new Point(p.X, p.Y);

                    indexOfEditingLineSegment = next;
                    (previous, next) = CalculatePreviousAndNextIndexOfLineSegment();
                }
                p.X = myPolygons[indexOfEditingPolygon].points[next].X + xChange;
                p.Y = myPolygons[indexOfEditingPolygon].points[next].Y + yChange;
                myPolygons[indexOfEditingPolygon].points[next] = new Point(p.X, p.Y);
            }


            List<LineSegment> lineSegments = GenerateLinesegmentsFromPoints(myPolygons[indexOfEditingPolygon].points);
            for (int i = 0; i < lineSegments.Count; i++)
            {
                if (myPolygons[indexOfEditingPolygon].lineSegments[i].verticalRestriction)
                {
                    lineSegments[i].AssignVerticalHestriction();
                }
                else if (myPolygons[indexOfEditingPolygon].lineSegments[i].horizontalRestriction)
                {
                    lineSegments[i].AssignHorizontalHestriction();
                }
                else if (myPolygons[indexOfEditingPolygon].lineSegments[i].lengthRestriction)
                {
                    lineSegments[i].AssignLengthHestriction();
                }
            }
            myPolygons[indexOfEditingPolygon] = new MyPolygon(lineSegments);

            pictureBox.Invalidate();
        }

        public (int, int) CalculatePreviousAndNextIndexOfLineSegment()
        {
            int previous = indexOfEditingLineSegment - 1;
            int next = indexOfEditingLineSegment + 1;

            if (indexOfEditingLineSegment == 0)
                previous = myPolygons[indexOfEditingPolygon].lineSegments.Count - 1;

            else if (indexOfEditingLineSegment == myPolygons[indexOfEditingPolygon].lineSegments.Count - 1)
                next = 0;

            return (previous, next);
        }

        public int CalculateNextIndexOfVertex(int index)
        {
            if (index == myPolygons[indexOfEditingPolygon].points.Count - 1)
                return 0;
            else
                return index + 1;
        }

        public int CalculatePreviousIndexOfVertex(int index)
        {
            if (index == 0)
                return myPolygons[indexOfEditingPolygon].points.Count - 1;
            else
                return index - 1;
        }

        public int CalculateIndexOfPreviousLineSegmentFromPoint(int index)
        {
            if (index == 0)
                return myPolygons[indexOfEditingPolygon].lineSegments.Count - 1;
            else
                return index - 1;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Do rysowania wielokąta służy lewy przycisk myszy, w miejsu jego naciśnięcia powstaje nowy wierzchołek." +
                "\n2. Aby zakończyć rysowanie wielokąta należy zakończyć rysowanie w wierzchołku początkowym." +
                "\n3. Aby anulować rysowanie wielokąta należy nacisnąć klawisz 'c' na klawiaturze." +
                "\n4. Aby przesunąć dany element (wierzchołek, krawędź lub cały wielokąt) należy na niego kliknąć " +
                "lewym przyciskiem myszy i trzymając go przsunąć kursor myszy." +
                "\n5. Aby wykonać dodatkowe funkcje (usuwanie elementu, wstawienie wierzchołka, edycja relacji) należy" +
                "na dany elemrnt kliknać prawym przyciskiem myszy i z kontekstowego menu wybrać pożądaną opcję.", 
                "Klawiszologia i sterowanie w aplikacji");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aplikacja służy do rysowania oraz edycji wielokątów. Jej działania opiera się w głównej mierze" +
                " na zaimplementowanym algorytmie Bresenhama oraz algorytmie pozwalającym zachować poszczególne ograniczenia i relacje " +
                "podczas edycji wielokąta." +
                "\n \n Autor: Wojciech Krawczyk", "About");
        }

        private void radioButtonLibAlg_CheckedChanged(object sender, EventArgs e)
        {
            typeOfDrawingAlgoritm = 0;
            drawingManagement.ChangeTypeOfDrawingAlgoritm(0);
            pictureBox.Invalidate();
        }

        private void radioButtonBreAlg_CheckedChanged(object sender, EventArgs e)
        {
            typeOfDrawingAlgoritm = 1;
            drawingManagement.ChangeTypeOfDrawingAlgoritm(1);
            pictureBox.Invalidate();
        }

        private void radioButtonWuAlg_CheckedChanged(object sender, EventArgs e)
        {
            typeOfDrawingAlgoritm = 2;
            drawingManagement.ChangeTypeOfDrawingAlgoritm(2);
            pictureBox.Invalidate();
        }

        public void GenerateStartPolygon()
        {
            List<Point> points = new List<Point>();
            Point p1 = new Point(650, 200);
            Point p2 = new Point(800, 250);
            Point p3 = new Point(800, 400);
            Point p4 = new Point(400, 400);
            Point p5 = new Point(300, 325);
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            points.Add(p5);
            List<LineSegment> lineSegments = GenerateLinesegmentsFromPoints(points);
            lineSegments[1].AssignVerticalHestriction();
            lineSegments[2].AssignHorizontalHestriction();
            lineSegments[3].AssignLengthHestriction();
            MyPolygon polygon = new MyPolygon(lineSegments);
            myPolygons.Add(polygon);
            Object sender = new object();
            MouseEventArgs e = new MouseEventArgs(MouseButtons.Left, 1, 600, 400, 0);
            pictureBox_MouseDown(sender, e);
            pictureBox_MouseUp(sender, e);
            pictureBox.Invalidate();
        }
    }

    public static class Bresenham
    {
        public static void MyLineBresenham(Point p1, Point p2, Graphics graphics)
        {
            int x1 = p1.X, x2 = p2.X;
            int y1 = p1.Y, y2 = p2.Y;

            int d, dx, dy, ai, bi, xi, yi;
            int x = x1, y = y1;
            // ustalenie kierunku rysowania
            if (x1 < x2)
            {
                xi = 1;
                dx = x2 - x1;
            }
            else
            {
                xi = -1;
                dx = x1 - x2;
            }
            // ustalenie kierunku rysowania
            if (y1 < y2)
            {
                yi = 1;
                dy = y2 - y1;
            }
            else
            {
                yi = -1;
                dy = y1 - y2;
            }
            // pierwszy piksel
            graphics.DrawRectangle(Pens.Black, x, y, 1, 1);
            // oś wiodąca OX
            if (dx > dy)
            {
                ai = (dy - dx) * 2;
                bi = dy * 2;
                d = bi - dx;
                // pętla po kolejnych x
                while (x != x2)
                {
                    // test współczynnika
                    if (d >= 0)
                    {
                        x += xi;
                        y += yi;
                        d += ai;
                    }
                    else
                    {
                        d += bi;
                        x += xi;
                    }
                    graphics.DrawRectangle(Pens.Black, x, y, 1, 1);
                }
            }
            // oś wiodąca OY
            else
            {
                ai = (dx - dy) * 2;
                bi = dx * 2;
                d = bi - dy;
                // pętla po kolejnych y
                while (y != y2)
                {
                    // test współczynnika
                    if (d >= 0)
                    {
                        x += xi;
                        y += yi;
                        d += ai;
                    }
                    else
                    {
                        d += bi;
                        y += yi;
                    }
                    graphics.DrawRectangle(Pens.Black, x, y, 1, 1);
                }
            }
        }
    }

    public class WuDrawing
    {
        public static void MyLineWu(Point p1, Point p2, Graphics graphics)
        {
            int x0 = p1.X, x1 = p2.X;
            int y0 = p1.Y, y1 = p2.Y;

            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);

            if (steep)
            {
                //swap x0 and y0
                int x = x0;
                x0 = y0;
                y0 = x;
                //swap x1 and y1
                int y = y1;
                y1 = x1;
                x1 = y;
            }
            if (x0 > x1)
            {
                //swap x0 and x1
                int x = x0;
                x0 = x1;
                x1 = x;
                //swap y0 and y1
                int y = y0;
                y0 = y1;
                y1 = y;
            }

            double dx = x1 - x0;
            double dy = y1 - y0;
            double gradient = dy / dx;
            if (dx == 0.0)
                gradient = 1;

            int xpxl1 = x0;
            int xpxl2 = x1;
            double intersectY = y0;

            if (steep)
            {
                int x;
                for (x = xpxl1; x <= xpxl2; x++)
                {
                    drawPixel(iPartOfNumber(intersectY), x, rfPartOfNumber(intersectY), graphics);
                    drawPixel(iPartOfNumber(intersectY) - 1, x, fPartOfNumber(intersectY), graphics);
                    intersectY += gradient;
                }
            }
            else
            {
                int x;
                for (x = xpxl1; x <= xpxl2; x++)
                {
                    // pixel coverage is determined by fractional 
                    // part of y co-ordinate 
                    drawPixel(x, iPartOfNumber(intersectY), rfPartOfNumber(intersectY), graphics);
                    drawPixel(x, iPartOfNumber(intersectY) - 1, fPartOfNumber(intersectY), graphics);
                    intersectY += gradient;
                }
            }
        }

        private static int iPartOfNumber(double x)
        {
            return (int)x;
        }

        private static int roundNumber(double x)
        {
            return iPartOfNumber(x + 0.5);
        }

        private static double fPartOfNumber(double x)
        {
            if (x > 0)
                return x - iPartOfNumber(x);
            return x - (iPartOfNumber(x) + 1);
        }

        private static double rfPartOfNumber(double x)
        {
            return 1 - fPartOfNumber(x);
        }

        private static void drawPixel(int x, int y, double brightness, Graphics g)
        {
            int c = (int)(255 * brightness);
            Color color = Color.FromArgb(255, c, c, c);
            g.DrawRectangle(new Pen(color), x, y, 1, 1);
        }
    }

    public class DrawingManagement
    {
        public bool active = false;
        public bool isEnd = false;
        private Point startPolygon;
        private Point startLineSegment;
        private Point endLineSegment = new Point(-1, -1);
        public List<LineSegment> lineSegments = new List<LineSegment>();
        public int typeOfDrawingAlgorithm = 0;

        public void ConsiderVertex(Point p, PictureBox picBoxVer, PictureBox picBoxHor, PictureBox picBoxLen)
        {
            if (!active)
            {
                StartPolygon(p);
            }
            else if (PointsDistance(startPolygon, p) < 10 && lineSegments.Count >= 2)
            {
                EndPolygon(p, picBoxVer, picBoxHor, picBoxLen);
            }
            else if (PointsDistance(startLineSegment, p) > 10)
            {
                AddVertex(p, picBoxVer, picBoxHor, picBoxLen);
            }
        }

        public void Moving(Point p)
        {
            this.endLineSegment = p;
        }

        public void Draw(Graphics graphics)
        {
            if (active && lineSegments.Count == 0)
                LineSegment.DrawPoint(graphics, Pens.Blue, startPolygon);
            foreach (var s in lineSegments)
                s.Draw(graphics, Pens.Black, typeOfDrawingAlgorithm);
            if (endLineSegment.X != -1)
            {
                if (typeOfDrawingAlgorithm == 0)
                    graphics.DrawLine(Pens.Black, startLineSegment, endLineSegment);
                else if (typeOfDrawingAlgorithm == 1)
                    Bresenham.MyLineBresenham(startLineSegment, endLineSegment, graphics);
                else if (typeOfDrawingAlgorithm == 2)
                    WuDrawing.MyLineWu(startLineSegment, endLineSegment, graphics);
            }
        }

        public void ChangeTypeOfDrawingAlgoritm(int typeOfAlg)
        {
            this.typeOfDrawingAlgorithm = typeOfAlg;
        }

        private void StartPolygon(Point p)
        {
            this.active = true;
            this.startPolygon = p;
            this.startLineSegment = p;
        }

        private void AddVertex(Point p, PictureBox picBoxVer, PictureBox picBoxHor, PictureBox picBoxLen)
        {
            this.lineSegments.Add(new LineSegment(startLineSegment, p, picBoxVer.Image, picBoxHor.Image, picBoxLen.Image));
            this.startLineSegment = p;
        }

        private void EndPolygon(Point p, PictureBox picBoxVer, PictureBox picBoxHor, PictureBox picBoxLen)
        {
            this.isEnd = true;
            this.lineSegments.Add(new LineSegment(startLineSegment, startPolygon, picBoxVer.Image, picBoxHor.Image, picBoxLen.Image));
        }

        public void Reset()
        {
            this.active = false;
            this.isEnd = false;
            this.lineSegments.Clear();
            this.endLineSegment = new Point(-1, -1);
        }

        public static int PointsDistance(Point p1, Point p2)
        {
            return (int)Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }
    }

    public class LineSegment
    {
        public Point p1;
        public Point p2;
        private Pen pen = new Pen(Color.Blue);

        public bool verticalRestriction = false;
        public bool horizontalRestriction = false;
        public bool lengthRestriction = false;

        private Image ver;
        private Image hor;
        private Image len;
        private Image image;

        public LineSegment() { }
        public LineSegment(Point p1, Point p2, Image ver, Image hor, Image len)
        {
            this.p1 = p1;
            this.p2 = p2;

            this.ver = ver;
            this.hor = hor;
            this.len = len;
        }

        public void Draw(Graphics graphics, Pen pen, int typeOfDrawingAlgorithm)
        {
            if (typeOfDrawingAlgorithm == 0)
                graphics.DrawLine(pen, p1, p2);
            else if (typeOfDrawingAlgorithm == 1)
                Bresenham.MyLineBresenham(p1, p2, graphics);
            else if (typeOfDrawingAlgorithm == 2)
                WuDrawing.MyLineWu(p1, p2, graphics);
            DrawPoint(graphics, this.pen, p1);
            DrawPoint(graphics, this.pen, p2);

            if (HasRestriction())
                graphics.DrawImage(image, new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2));
        }

        public static void DrawPoint(Graphics graphics, Pen pen, Point p)
        {
            graphics.DrawEllipse(pen, p.X - 2, p.Y - 2, 4, 4);
            graphics.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.Blue), p.X - 2, p.Y - 2, 4, 4);
        }

        public bool HasRestriction()
        {
            return verticalRestriction || horizontalRestriction || lengthRestriction;
        }

        public void AssignVerticalHestriction()
        {
            verticalRestriction = true;
            image = ver;
        }

        public void AssignHorizontalHestriction()
        {
            horizontalRestriction = true;
            image = hor;
        }

        public void AssignLengthHestriction()
        {
            lengthRestriction = true;
            image = len;
        }

        public void ResetResriction()
        {
            verticalRestriction = false;
            horizontalRestriction = false;
            lengthRestriction = false;
        }
    }

    public class MyPolygon
    {
        public List<LineSegment> lineSegments;
        public List<Point> points = new List<Point>();

        public MyPolygon(List<LineSegment> lineSegments)
        {
            this.lineSegments = new List<LineSegment>(lineSegments);
            foreach (var ls in lineSegments)
                this.points.Add(ls.p1);
        }
        
        public void Draw(Graphics graphics, Pen pen, int typeOfDrawingAlgorithm)
        {
            Brush brush = new SolidBrush(Color.FromArgb(100, Color.CornflowerBlue));
            graphics.FillPolygon(brush, points.ToArray());
            foreach (var ls in lineSegments)
                ls.Draw(graphics, pen, typeOfDrawingAlgorithm);
        }
    }
}
