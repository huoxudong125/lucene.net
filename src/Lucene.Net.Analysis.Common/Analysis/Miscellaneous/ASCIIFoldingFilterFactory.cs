﻿using System.Collections.Generic;
using Lucene.Net.Analysis.Util;
using TokenFilterFactory = Lucene.Net.Analysis.Util.TokenFilterFactory;

namespace org.apache.lucene.analysis.miscellaneous
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

	using AbstractAnalysisFactory = AbstractAnalysisFactory;
	using MultiTermAwareComponent = org.apache.lucene.analysis.util.MultiTermAwareComponent;
	using TokenFilterFactory = TokenFilterFactory;

	/// <summary>
	/// Factory for <seealso cref="ASCIIFoldingFilter"/>.
	/// <pre class="prettyprint">
	/// &lt;fieldType name="text_ascii" class="solr.TextField" positionIncrementGap="100"&gt;
	///   &lt;analyzer&gt;
	///     &lt;tokenizer class="solr.WhitespaceTokenizerFactory"/&gt;
	///     &lt;filter class="solr.ASCIIFoldingFilterFactory" preserveOriginal="false"/&gt;
	///   &lt;/analyzer&gt;
	/// &lt;/fieldType&gt;</pre>
	/// </summary>
	public class ASCIIFoldingFilterFactory : TokenFilterFactory, MultiTermAwareComponent
	{
	  private readonly bool preserveOriginal;

	  /// <summary>
	  /// Creates a new ASCIIFoldingFilterFactory </summary>
	  public ASCIIFoldingFilterFactory(IDictionary<string, string> args) : base(args)
	  {
		preserveOriginal = getBoolean(args, "preserveOriginal", false);
		if (args.Count > 0)
		{
		  throw new System.ArgumentException("Unknown parameters: " + args);
		}
	  }

	  public override ASCIIFoldingFilter create(TokenStream input)
	  {
		return new ASCIIFoldingFilter(input, preserveOriginal);
	  }

	  public virtual AbstractAnalysisFactory MultiTermComponent
	  {
		  get
		  {
			return this;
		  }
	  }
	}


}