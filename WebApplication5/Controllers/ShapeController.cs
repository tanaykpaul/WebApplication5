using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Http;
using WebApplication5.Triangles;

namespace WebApplication5.Controllers
{
    public class ShapeController : ApiController
    {
        private TriangleType _triangleType;
        private TriangleDimensions _triangleDimensions;

        public string Get()
        {
            return "I am healthy... from the Shape Controller.";
        }

        [HttpGet]
        public TriangleOutcome Get(string triagleDescription)
        {
            if (!IsParsed(triagleDescription))
            {
                return null;
            }

            switch (_triangleType)
            {
                case TriangleType.Isosceles:
                    return new Isosceles().GetSides(_triangleDimensions);
                case TriangleType.Scalene:
                    return new Scalene().GetSides(_triangleDimensions);
                case TriangleType.Equilateral:
                    return new Equilateral().GetSides(_triangleDimensions);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Validate the description
        /// </summary>
        /// <param name="description">Front-end input</param>
        /// <returns></returns>
        private bool IsParsed(string description)
        {
            int nextHeight = 0, nextBase = 0, nextSide1 = 0, nextSide2 = 0, nextSide3 = 0;
            var words = Regex.Matches(description, @"\b(?:[a-z]{2,}|[ai])\b|\d+", RegexOptions.IgnoreCase);
            _triangleType = TriangleType.Undefined;
            _triangleDimensions = new TriangleDimensions {Height = -1, Base = -1};

            foreach (var word in words)
            {
                if (_triangleType == TriangleType.Undefined)
                {
                    if (string.Equals(word.ToString(), TriangleType.Equilateral.ToString(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        _triangleType = TriangleType.Equilateral;
                    }
                    else if (string.Equals(word.ToString(), TriangleType.Isosceles.ToString(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        _triangleType = TriangleType.Isosceles;
                    }
                    else if (string.Equals(word.ToString(), TriangleType.Scalene.ToString(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        _triangleType = TriangleType.Scalene;
                    }
                }
                else
                {
                    if (_triangleType == TriangleType.Isosceles)
                    {
                        int value;
                        if (nextHeight == 0 && word.ToString().ToLower() == "height")
                        {
                            nextHeight = 1;
                        }
                        else if (nextBase == 0 && word.ToString().ToLower() == "base")
                        {
                            nextBase = 1;
                        }
                        else if (int.TryParse(word.ToString(), out value))
                        {
                            if (nextHeight == 1)
                            {
                                _triangleDimensions.Height = value;
                                nextHeight = 2;
                            }
                            else if (nextBase == 1)
                            {
                                _triangleDimensions.Base = value;
                                nextBase = 2;
                            }
                        }
                    }
                    else if (_triangleType == TriangleType.Equilateral)
                    {
                        int value;
                        if (nextSide1 == 0 && word.ToString().ToLower() == "side")
                        {
                            nextSide1 = 1;
                        }
                        else if (int.TryParse(word.ToString(), out value))
                        {
                            if (nextSide1 == 1)
                            {
                                _triangleDimensions.Sides = new List<int> {value};
                                nextSide1 = 2;
                            }
                        }
                    }
                    else if (_triangleType == TriangleType.Scalene)
                    {
                        int value;
                        if (nextSide1 == 0 && word.ToString().ToLower() == "side")
                        {
                            nextSide1 = 1;
                        }
                        else if (nextSide2 == 0 && word.ToString().ToLower() == "side")
                        {
                            nextSide2 = 1;
                        }
                        else if (nextSide3 == 0 && word.ToString().ToLower() == "side")
                        {
                            nextSide3 = 1;
                        }
                        else if (word.ToString().ToLower() == "side")
                        {
                            // Max 3 sides are valid
                            return false;
                        }
                        else if (int.TryParse(word.ToString(), out value))
                        {
                            if (nextSide1 == 1)
                            {
                                _triangleDimensions.Sides = new List<int> { value };
                                nextSide1 = 2;
                            }
                            else if (nextSide2 == 1)
                            {
                                _triangleDimensions.Sides.Add(value);
                                nextSide2 = 2;
                            }
                            else if (nextSide3 == 1)
                            {
                                _triangleDimensions.Sides.Add(value);
                                nextSide3 = 2;
                            }
                        }
                    }
                }
            }

            return _triangleType != TriangleType.Undefined;
        }
    }
}