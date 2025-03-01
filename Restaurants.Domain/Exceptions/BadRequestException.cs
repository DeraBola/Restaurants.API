using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Exceptions;


public class BadRequestException(string v) : Exception("Invalid input data") { }


