﻿namespace org.apache.lucene.analysis.compound
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


	using CharArraySet = org.apache.lucene.analysis.util.CharArraySet;
	using Version = org.apache.lucene.util.Version;

	/// <summary>
	/// A <seealso cref="TokenFilter"/> that decomposes compound words found in many Germanic languages.
	/// <para>
	/// "Donaudampfschiff" becomes Donau, dampf, schiff so that you can find
	/// "Donaudampfschiff" even when you only enter "schiff". 
	///  It uses a brute-force algorithm to achieve this.
	/// </para>
	/// <para>
	/// You must specify the required <seealso cref="Version"/> compatibility when creating
	/// CompoundWordTokenFilterBase:
	/// <ul>
	/// <li>As of 3.1, CompoundWordTokenFilterBase correctly handles Unicode 4.0
	/// supplementary characters in strings and char arrays provided as compound word
	/// dictionaries.
	/// </ul>
	/// </para>
	/// </summary>
	public class DictionaryCompoundWordTokenFilter : CompoundWordTokenFilterBase
	{

	  /// <summary>
	  /// Creates a new <seealso cref="DictionaryCompoundWordTokenFilter"/>
	  /// </summary>
	  /// <param name="matchVersion">
	  ///          Lucene version to enable correct Unicode 4.0 behavior in the
	  ///          dictionaries if Version > 3.0. See <a
	  ///          href="CompoundWordTokenFilterBase.html#version"
	  ///          >CompoundWordTokenFilterBase</a> for details. </param>
	  /// <param name="input">
	  ///          the <seealso cref="TokenStream"/> to process </param>
	  /// <param name="dictionary">
	  ///          the word dictionary to match against. </param>
	  public DictionaryCompoundWordTokenFilter(Version matchVersion, TokenStream input, CharArraySet dictionary) : base(matchVersion, input, dictionary)
	  {
		if (dictionary == null)
		{
		  throw new System.ArgumentException("dictionary cannot be null");
		}
	  }

	  /// <summary>
	  /// Creates a new <seealso cref="DictionaryCompoundWordTokenFilter"/>
	  /// </summary>
	  /// <param name="matchVersion">
	  ///          Lucene version to enable correct Unicode 4.0 behavior in the
	  ///          dictionaries if Version > 3.0. See <a
	  ///          href="CompoundWordTokenFilterBase.html#version"
	  ///          >CompoundWordTokenFilterBase</a> for details. </param>
	  /// <param name="input">
	  ///          the <seealso cref="TokenStream"/> to process </param>
	  /// <param name="dictionary">
	  ///          the word dictionary to match against. </param>
	  /// <param name="minWordSize">
	  ///          only words longer than this get processed </param>
	  /// <param name="minSubwordSize">
	  ///          only subwords longer than this get to the output stream </param>
	  /// <param name="maxSubwordSize">
	  ///          only subwords shorter than this get to the output stream </param>
	  /// <param name="onlyLongestMatch">
	  ///          Add only the longest matching subword to the stream </param>
	  public DictionaryCompoundWordTokenFilter(Version matchVersion, TokenStream input, CharArraySet dictionary, int minWordSize, int minSubwordSize, int maxSubwordSize, bool onlyLongestMatch) : base(matchVersion, input, dictionary, minWordSize, minSubwordSize, maxSubwordSize, onlyLongestMatch)
	  {
		if (dictionary == null)
		{
		  throw new System.ArgumentException("dictionary cannot be null");
		}
	  }

	  protected internal override void decompose()
	  {
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final int len = termAtt.length();
		int len = termAtt.length();
		for (int i = 0;i <= len - this.minSubwordSize;++i)
		{
			CompoundToken longestMatchToken = null;
			for (int j = this.minSubwordSize;j <= this.maxSubwordSize;++j)
			{
				if (i + j > len)
				{
					break;
				}
				if (dictionary.contains(termAtt.buffer(), i, j))
				{
					if (this.onlyLongestMatch)
					{
					   if (longestMatchToken != null)
					   {
						 if (longestMatchToken.txt.length() < j)
						 {
						   longestMatchToken = new CompoundToken(this, i,j);
						 }
					   }
					   else
					   {
						 longestMatchToken = new CompoundToken(this, i,j);
					   }
					}
					else
					{
					   tokens.AddLast(new CompoundToken(this, i,j));
					}
				}
			}
			if (this.onlyLongestMatch && longestMatchToken != null)
			{
			  tokens.AddLast(longestMatchToken);
			}
		}
	  }
	}

}