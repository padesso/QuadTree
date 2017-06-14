﻿using QuadTreeTDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuadTreeVisualizerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private QuadTree quadTree;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AxisAlignedBoundingBox outerBounds = new AxisAlignedBoundingBox(
                new QuadTreeTDD.Vector((float)VisualizerCanvas.ActualWidth / 2.0f, (float)VisualizerCanvas.ActualHeight / 2.0f), (float)VisualizerCanvas.ActualWidth, 
                (float)VisualizerCanvas.ActualHeight);

            quadTree = new QuadTree(outerBounds);

            Random rand = new Random(DateTime.Now.Millisecond);
            for (int posIndex = 0; posIndex < 50; posIndex++)
            {
                QuadTreeTDD.Vector tempPos = new QuadTreeTDD.Vector(rand.Next(0, (int)VisualizerCanvas.ActualWidth), rand.Next(0, (int)VisualizerCanvas.ActualHeight));
                quadTree.Insert(tempPos);
            }

            DrawQuadTreeBounds(quadTree);
            DrawQuadTreePositions(quadTree);

            Console.WriteLine("Wait");
        }

        private void DrawQuadTreePositions(QuadTree quad)
        {
            //Get all the points
            List<QuadTreeTDD.Vector> allPositions = quad.QueryBounds(quad.Bounds);

            //Draw the points
            foreach (QuadTreeTDD.Vector pos in allPositions)
            {
                Ellipse ellipse = new Ellipse
                {
                    StrokeThickness = 1,
                    Stroke = Brushes.Black,
                    Width = 3,
                    Height = 3
                };
                VisualizerCanvas.Children.Add(ellipse);
                Canvas.SetLeft(ellipse, pos.X - ellipse.Width * 0.5);
                Canvas.SetTop(ellipse, pos.Y - ellipse.Height * 0.5);
            }
        }

        private void DrawQuadTreeBounds(QuadTree quad)
        {
            //Draw the bounding rectanlges
            Rectangle rect = new Rectangle
            {
                StrokeThickness = 1,
                Stroke = Brushes.Black,
                Width = quad.Bounds.Width,
                Height = quad.Bounds.Height
            };
            VisualizerCanvas.Children.Add(rect);
            Canvas.SetLeft(rect, quad.Bounds.TopLeft.X);
            Canvas.SetTop(rect, quad.Bounds.TopLeft.Y);
                
            //Recusrively draw the children of this quad
            if (quad.NorthWest != null)
                DrawQuadTreeBounds(quad.NorthWest);

            if (quad.NorthEast != null)
                DrawQuadTreeBounds(quad.NorthEast);

            if (quad.SouthWest != null)
                DrawQuadTreeBounds(quad.SouthWest);

            if (quad.SouthEast != null)
                DrawQuadTreeBounds(quad.SouthEast);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Escape if there is no quad
            if (quadTree == null)
                return;

            //Clear the old canvas state
            VisualizerCanvas.Children.Clear();

            //Get all the points so we can re-insert after the resize
            List<QuadTreeTDD.Vector> allPositions = quadTree.QueryBounds(quadTree.Bounds);

            //Get the new size
            AxisAlignedBoundingBox newBounds = new AxisAlignedBoundingBox(
                new QuadTreeTDD.Vector((float)VisualizerCanvas.ActualWidth / 2.0f, (float)VisualizerCanvas.ActualHeight / 2.0f), (float)VisualizerCanvas.ActualWidth,
                (float)VisualizerCanvas.ActualHeight);

            //Recreate the QuadTree with the new window size
            quadTree = new QuadTree(newBounds);

            //Re-insert the positions
            foreach(QuadTreeTDD.Vector pos in allPositions)
            {
                quadTree.Insert(pos);
            }

            //Redraw
            DrawQuadTreeBounds(quadTree);
            DrawQuadTreePositions(quadTree);
        }
    }
}
