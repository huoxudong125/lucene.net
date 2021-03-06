﻿using System;

namespace org.apache.lucene.analysis.util
{

	/*
	 * Licensed to the Apache Software Foundation (ASF) under one or more
	 * contributor license agreements.  See the NOTICE file distributed with
	 * this work for additional information regarding copyright ownership.
	 * The ASF licenses this file to You under the Apache License, Version 2.0
	 * (the "License"); you may not use this file except in compliance with
	 * the License.  You may obtain a copy of the License at
	 *
	 *     http://www.apache.org/licenses/LICENSE-2.0
	 *
	 * Unless required by applicable law or agreed to in writing, software
	 * distributed under the License is distributed on an "AS IS" BASIS,
	 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	 * See the License for the specific language governing permissions and
	 * limitations under the License.
	 */


	/// <summary>
	/// Abstraction for loading resources (streams, files, and classes).
	/// </summary>
	public interface ResourceLoader
	{

	  /// <summary>
	  /// Opens a named resource
	  /// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public java.io.InputStream openResource(String resource) throws java.io.IOException;
	  InputStream openResource(string resource);


	  /// <summary>
	  /// Finds class of the name and expected type
	  /// </summary>
	  Type findClass<T>(string cname, Type expectedType);

	  /// <summary>
	  /// Creates an instance of the name and expected type
	  /// </summary>
	  // TODO: fix exception handling
	  T newInstance<T>(string cname, Type expectedType);
	}
}