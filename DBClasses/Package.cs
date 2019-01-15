﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClasses {
	public class Package {

		public Package() {
			
		}
		public int PackageId{get; set;}
		public string PkgName { get; set; }
		public DateTime PkgStartDate { get; set; }
		public DateTime PkgEndDate { get; set; }
		public string PkgDesc { get; set; }
		public decimal PkgBasePrice { get; set; }
		public decimal PkgAgencyCommission { get; set; }

	}
}
