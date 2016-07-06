 /*
  * Coded in part in SharpDevelop
  * Code by: Jakob Marasch
  * 		 newterrastudios@sbcglobal.net
  * Date:    7/29/2009
  * Time:    3:05 PM
  * Project: NewTerra 
  * 
  */
using System;

namespace NTAF.UniverseBuilder.WinGui
{
//	/// <summary>
//	/// Used to raise an exeception when a data control cannot be found
//	/// </summary>
//	public class DataNotFoundException : System.Exception {
//        public DataNotFoundException() { }
//        public DataNotFoundException( string message ) : base( message ) { }
//        public DataNotFoundException( string message, Exception innerEcecption ) : base( message, innerEcecption ) { }
//    }
	/// <summary>
	/// ugh, really? the name should spell it out...
	/// </summary>
	public class NothingSelectedException : System.Exception {
        public NothingSelectedException() { }
        public NothingSelectedException( string message ) : base( message ) { }
        public NothingSelectedException( string message, Exception innerEcecption ) : base( message, innerEcecption ) { }
    }
}
