﻿namespace org.apache.lucene.analysis.miscellaneous
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
	using CharTermAttribute = org.apache.lucene.analysis.tokenattributes.CharTermAttribute;
	using KeywordAttribute = org.apache.lucene.analysis.tokenattributes.KeywordAttribute;
	using CharArraySet = org.apache.lucene.analysis.util.CharArraySet;

	/// <summary>
	/// Marks terms as keywords via the <seealso cref="KeywordAttribute"/>. Each token
	/// contained in the provided set is marked as a keyword by setting
	/// <seealso cref="KeywordAttribute#setKeyword(boolean)"/> to <code>true</code>.
	/// </summary>
	public sealed class SetKeywordMarkerFilter : KeywordMarkerFilter
	{
	  private readonly CharTermAttribute termAtt = addAttribute(typeof(CharTermAttribute));
	  private readonly CharArraySet keywordSet;

	  /// <summary>
	  /// Create a new KeywordSetMarkerFilter, that marks the current token as a
	  /// keyword if the tokens term buffer is contained in the given set via the
	  /// <seealso cref="KeywordAttribute"/>.
	  /// </summary>
	  /// <param name="in">
	  ///          TokenStream to filter </param>
	  /// <param name="keywordSet">
	  ///          the keywords set to lookup the current termbuffer </param>
//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
//ORIGINAL LINE: public SetKeywordMarkerFilter(final org.apache.lucene.analysis.TokenStream in, final org.apache.lucene.analysis.util.CharArraySet keywordSet)
	  public SetKeywordMarkerFilter(TokenStream @in, CharArraySet keywordSet) : base(@in)
	  {
		this.keywordSet = keywordSet;
	  }

	  protected internal override bool Keyword
	  {
		  get
		  {
			return keywordSet.contains(termAtt.buffer(), 0, termAtt.length());
		  }
	  }

	}

}