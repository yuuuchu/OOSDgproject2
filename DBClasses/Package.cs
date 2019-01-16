﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Project: OOSD Threaded Project 2
* Package class with getters and setters
* Author: Hayden Belanger
* Date: Jan 2019
* Commenter: Eugenia Chiu
*/

namespace DBClasses {

    //Make package class
	public class Package {

        //Default empty constructor for class
		public Package() {
			
		}
        //public methods for getters and setters
        //Note the type values are different and reflects database values
		public int PackageId{get; set;}
		public string PkgName { get; set; }
		public DateTime PkgStartDate { get; set; }
		public DateTime PkgEndDate { get; set; }
		public string PkgDesc { get; set; }
		public decimal PkgBasePrice { get; set; }
		public decimal PkgAgencyCommission { get; set; }

	}
}
