﻿using System.Diagnostics;

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


	using Version = org.apache.lucene.util.Version;

	/// <summary>
	/// <seealso cref="CharacterUtils"/> provides a unified interface to Character-related
	/// operations to implement backwards compatible character operations based on a
	/// <seealso cref="Version"/> instance.
	/// 
	/// @lucene.internal
	/// </summary>
	public abstract class CharacterUtils
	{
	  private static readonly Java4CharacterUtils JAVA_4 = new Java4CharacterUtils();
	  private static readonly Java5CharacterUtils JAVA_5 = new Java5CharacterUtils();

	  /// <summary>
	  /// Returns a <seealso cref="CharacterUtils"/> implementation according to the given
	  /// <seealso cref="Version"/> instance.
	  /// </summary>
	  /// <param name="matchVersion">
	  ///          a version instance </param>
	  /// <returns> a <seealso cref="CharacterUtils"/> implementation according to the given
	  ///         <seealso cref="Version"/> instance. </returns>
//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
//ORIGINAL LINE: public static CharacterUtils getInstance(final org.apache.lucene.util.Version matchVersion)
	  public static CharacterUtils getInstance(Version matchVersion)
	  {
		return matchVersion.onOrAfter(Version.LUCENE_31) ? JAVA_5 : JAVA_4;
	  }

	  /// <summary>
	  /// Return a <seealso cref="CharacterUtils"/> instance compatible with Java 1.4. </summary>
	  public static CharacterUtils Java4Instance
	  {
		  get
		  {
			return JAVA_4;
		  }
	  }

	  /// <summary>
	  /// Returns the code point at the given index of the <seealso cref="CharSequence"/>.
	  /// Depending on the <seealso cref="Version"/> passed to
	  /// <seealso cref="CharacterUtils#getInstance(Version)"/> this method mimics the behavior
	  /// of <seealso cref="Character#codePointAt(char[], int)"/> as it would have been
	  /// available on a Java 1.4 JVM or on a later virtual machine version.
	  /// </summary>
	  /// <param name="seq">
	  ///          a character sequence </param>
	  /// <param name="offset">
	  ///          the offset to the char values in the chars array to be converted
	  /// </param>
	  /// <returns> the Unicode code point at the given index </returns>
	  /// <exception cref="NullPointerException">
	  ///           - if the sequence is null. </exception>
	  /// <exception cref="IndexOutOfBoundsException">
	  ///           - if the value offset is negative or not less than the length of
	  ///           the character sequence. </exception>
//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
//ORIGINAL LINE: public abstract int codePointAt(final CharSequence seq, final int offset);
	  public abstract int codePointAt(CharSequence seq, int offset);

	  /// <summary>
	  /// Returns the code point at the given index of the char array where only elements
	  /// with index less than the limit are used.
	  /// Depending on the <seealso cref="Version"/> passed to
	  /// <seealso cref="CharacterUtils#getInstance(Version)"/> this method mimics the behavior
	  /// of <seealso cref="Character#codePointAt(char[], int)"/> as it would have been
	  /// available on a Java 1.4 JVM or on a later virtual machine version.
	  /// </summary>
	  /// <param name="chars">
	  ///          a character array </param>
	  /// <param name="offset">
	  ///          the offset to the char values in the chars array to be converted </param>
	  /// <param name="limit"> the index afer the last element that should be used to calculate
	  ///        codepoint.  
	  /// </param>
	  /// <returns> the Unicode code point at the given index </returns>
	  /// <exception cref="NullPointerException">
	  ///           - if the array is null. </exception>
	  /// <exception cref="IndexOutOfBoundsException">
	  ///           - if the value offset is negative or not less than the length of
	  ///           the char array. </exception>
//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
//ORIGINAL LINE: public abstract int codePointAt(final char[] chars, final int offset, final int limit);
	  public abstract int codePointAt(char[] chars, int offset, int limit);

	  /// <summary>
	  /// Return the number of characters in <code>seq</code>. </summary>
	  public abstract int codePointCount(CharSequence seq);

	  /// <summary>
	  /// Creates a new <seealso cref="CharacterBuffer"/> and allocates a <code>char[]</code>
	  /// of the given bufferSize.
	  /// </summary>
	  /// <param name="bufferSize">
	  ///          the internal char buffer size, must be <code>&gt;= 2</code> </param>
	  /// <returns> a new <seealso cref="CharacterBuffer"/> instance. </returns>
//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
//ORIGINAL LINE: public static CharacterBuffer newCharacterBuffer(final int bufferSize)
	  public static CharacterBuffer newCharacterBuffer(int bufferSize)
	  {
		if (bufferSize < 2)
		{
		  throw new System.ArgumentException("buffersize must be >= 2");
		}
		return new CharacterBuffer(new char[bufferSize], 0, 0);
	  }


	  /// <summary>
	  /// Converts each unicode codepoint to lowerCase via <seealso cref="Character#toLowerCase(int)"/> starting 
	  /// at the given offset. </summary>
	  /// <param name="buffer"> the char buffer to lowercase </param>
	  /// <param name="offset"> the offset to start at </param>
	  /// <param name="limit"> the max char in the buffer to lower case </param>
//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
//ORIGINAL LINE: public final void toLowerCase(final char[] buffer, final int offset, final int limit)
	  public void ToLower(char[] buffer, int offset, int limit)
	  {
		Debug.Assert(buffer.Length >= limit);
		Debug.Assert(offset <= 0 && offset <= buffer.Length);
		for (int i = offset; i < limit;)
		{
		  i += char.toChars(char.ToLower(codePointAt(buffer, i, limit)), buffer, i);
		}
	  }

	  /// <summary>
	  /// Converts each unicode codepoint to UpperCase via <seealso cref="Character#toUpperCase(int)"/> starting 
	  /// at the given offset. </summary>
	  /// <param name="buffer"> the char buffer to UPPERCASE </param>
	  /// <param name="offset"> the offset to start at </param>
	  /// <param name="limit"> the max char in the buffer to lower case </param>
//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
//ORIGINAL LINE: public final void toUpperCase(final char[] buffer, final int offset, final int limit)
	  public void ToUpper(char[] buffer, int offset, int limit)
	  {
		Debug.Assert(buffer.Length >= limit);
		Debug.Assert(offset <= 0 && offset <= buffer.Length);
		for (int i = offset; i < limit;)
		{
		  i += char.toChars(char.ToUpper(codePointAt(buffer, i, limit)), buffer, i);
		}
	  }

	  /// <summary>
	  /// Converts a sequence of Java characters to a sequence of unicode code points. </summary>
	  ///  <returns> the number of code points written to the destination buffer  </returns>
	  public int toCodePoints(char[] src, int srcOff, int srcLen, int[] dest, int destOff)
	  {
		if (srcLen < 0)
		{
		  throw new System.ArgumentException("srcLen must be >= 0");
		}
		int codePointCount_Renamed = 0;
		for (int i = 0; i < srcLen;)
		{
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final int cp = codePointAt(src, srcOff + i, srcOff + srcLen);
		  int cp = codePointAt(src, srcOff + i, srcOff + srcLen);
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final int charCount = Character.charCount(cp);
		  int charCount = char.charCount(cp);
		  dest[destOff + codePointCount_Renamed++] = cp;
		  i += charCount;
		}
		return codePointCount_Renamed;
	  }

	  /// <summary>
	  /// Converts a sequence of unicode code points to a sequence of Java characters. </summary>
	  ///  <returns> the number of chars written to the destination buffer  </returns>
	  public int toChars(int[] src, int srcOff, int srcLen, char[] dest, int destOff)
	  {
		if (srcLen < 0)
		{
		  throw new System.ArgumentException("srcLen must be >= 0");
		}
		int written = 0;
		for (int i = 0; i < srcLen; ++i)
		{
		  written += char.toChars(src[srcOff + i], dest, destOff + written);
		}
		return written;
	  }

	  /// <summary>
	  /// Fills the <seealso cref="CharacterBuffer"/> with characters read from the given
	  /// reader <seealso cref="Reader"/>. This method tries to read <code>numChars</code>
	  /// characters into the <seealso cref="CharacterBuffer"/>, each call to fill will start
	  /// filling the buffer from offset <code>0</code> up to <code>numChars</code>.
	  /// In case code points can span across 2 java characters, this method may
	  /// only fill <code>numChars - 1</code> characters in order not to split in
	  /// the middle of a surrogate pair, even if there are remaining characters in
	  /// the <seealso cref="Reader"/>.
	  /// <para>
	  /// Depending on the <seealso cref="Version"/> passed to
	  /// <seealso cref="CharacterUtils#getInstance(Version)"/> this method implements
	  /// supplementary character awareness when filling the given buffer. For all
	  /// <seealso cref="Version"/> &gt; 3.0 <seealso cref="#fill(CharacterBuffer, Reader, int)"/> guarantees
	  /// that the given <seealso cref="CharacterBuffer"/> will never contain a high surrogate
	  /// character as the last element in the buffer unless it is the last available
	  /// character in the reader. In other words, high and low surrogate pairs will
	  /// always be preserved across buffer boarders.
	  /// </para>
	  /// <para>
	  /// A return value of <code>false</code> means that this method call exhausted
	  /// the reader, but there may be some bytes which have been read, which can be
	  /// verified by checking whether <code>buffer.getLength() &gt; 0</code>.
	  /// </para>
	  /// </summary>
	  /// <param name="buffer">
	  ///          the buffer to fill. </param>
	  /// <param name="reader">
	  ///          the reader to read characters from. </param>
	  /// <param name="numChars">
	  ///          the number of chars to read </param>
	  /// <returns> <code>false</code> if and only if reader.read returned -1 while trying to fill the buffer </returns>
	  /// <exception cref="IOException">
	  ///           if the reader throws an <seealso cref="IOException"/>. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public abstract boolean fill(CharacterBuffer buffer, java.io.Reader reader, int numChars) throws java.io.IOException;
	  public abstract bool fill(CharacterBuffer buffer, Reader reader, int numChars);

	  /// <summary>
	  /// Convenience method which calls <code>fill(buffer, reader, buffer.buffer.length)</code>. </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final boolean fill(CharacterBuffer buffer, java.io.Reader reader) throws java.io.IOException
	  public bool fill(CharacterBuffer buffer, Reader reader)
	  {
		return fill(buffer, reader, buffer.buffer.Length);
	  }

	  /// <summary>
	  /// Return the index within <code>buf[start:start+count]</code> which is by <code>offset</code>
	  ///  code points from <code>index</code>. 
	  /// </summary>
	  public abstract int offsetByCodePoints(char[] buf, int start, int count, int index, int offset);

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: static int readFully(java.io.Reader reader, char[] dest, int offset, int len) throws java.io.IOException
	  internal static int readFully(Reader reader, char[] dest, int offset, int len)
	  {
		int read = 0;
		while (read < len)
		{
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final int r = reader.read(dest, offset + read, len - read);
		  int r = reader.read(dest, offset + read, len - read);
		  if (r == -1)
		  {
			break;
		  }
		  read += r;
		}
		return read;
	  }

	  private sealed class Java5CharacterUtils : CharacterUtils
	  {
		internal Java5CharacterUtils()
		{
		}

//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
//ORIGINAL LINE: @Override public int codePointAt(final CharSequence seq, final int offset)
		public override int codePointAt(CharSequence seq, int offset)
		{
		  return char.codePointAt(seq, offset);
		}

//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
//ORIGINAL LINE: @Override public int codePointAt(final char[] chars, final int offset, final int limit)
		public override int codePointAt(char[] chars, int offset, int limit)
		{
		 return char.codePointAt(chars, offset, limit);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public boolean fill(final CharacterBuffer buffer, final java.io.Reader reader, int numChars) throws java.io.IOException
//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
		public override bool fill(CharacterBuffer buffer, Reader reader, int numChars)
		{
		  Debug.Assert(buffer.buffer.Length >= 2);
		  if (numChars < 2 || numChars > buffer.buffer.Length)
		  {
			throw new System.ArgumentException("numChars must be >= 2 and <= the buffer size");
		  }
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final char[] charBuffer = buffer.buffer;
		  char[] charBuffer = buffer.buffer;
		  buffer.offset = 0;
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final int offset;
		  int offset;

		  // Install the previously saved ending high surrogate:
		  if (buffer.lastTrailingHighSurrogate != 0)
		  {
			charBuffer[0] = buffer.lastTrailingHighSurrogate;
			buffer.lastTrailingHighSurrogate = (char)0;
			offset = 1;
		  }
		  else
		  {
			offset = 0;
		  }

//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final int read = readFully(reader, charBuffer, offset, numChars - offset);
		  int read = readFully(reader, charBuffer, offset, numChars - offset);

		  buffer.length = offset + read;
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final boolean result = buffer.length == numChars;
		  bool result = buffer.length == numChars;
		  if (buffer.length < numChars)
		  {
			// We failed to fill the buffer. Even if the last char is a high
			// surrogate, there is nothing we can do
			return result;
		  }

		  if (char.IsHighSurrogate(charBuffer[buffer.length - 1]))
		  {
			buffer.lastTrailingHighSurrogate = charBuffer[--buffer.length];
		  }
		  return result;
		}

		public override int codePointCount(CharSequence seq)
		{
		  return char.codePointCount(seq, 0, seq.length());
		}

		public override int offsetByCodePoints(char[] buf, int start, int count, int index, int offset)
		{
		  return char.offsetByCodePoints(buf, start, count, index, offset);
		}
	  }

	  private sealed class Java4CharacterUtils : CharacterUtils
	  {
		internal Java4CharacterUtils()
		{
		}

//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
//ORIGINAL LINE: @Override public int codePointAt(final CharSequence seq, final int offset)
		public override int codePointAt(CharSequence seq, int offset)
		{
		  return seq.charAt(offset);
		}

//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
//ORIGINAL LINE: @Override public int codePointAt(final char[] chars, final int offset, final int limit)
		public override int codePointAt(char[] chars, int offset, int limit)
		{
		  if (offset >= limit)
		  {
			throw new System.IndexOutOfRangeException("offset must be less than limit");
		  }
		  return chars[offset];
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public boolean fill(CharacterBuffer buffer, java.io.Reader reader, int numChars) throws java.io.IOException
		public override bool fill(CharacterBuffer buffer, Reader reader, int numChars)
		{
		  Debug.Assert(buffer.buffer.Length >= 1);
		  if (numChars < 1 || numChars > buffer.buffer.Length)
		  {
			throw new System.ArgumentException("numChars must be >= 1 and <= the buffer size");
		  }
		  buffer.offset = 0;
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final int read = readFully(reader, buffer.buffer, 0, numChars);
		  int read = readFully(reader, buffer.buffer, 0, numChars);
		  buffer.length = read;
		  buffer.lastTrailingHighSurrogate = (char)0;
		  return read == numChars;
		}

		public override int codePointCount(CharSequence seq)
		{
		  return seq.length();
		}

		public override int offsetByCodePoints(char[] buf, int start, int count, int index, int offset)
		{
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final int result = index + offset;
		  int result = index + offset;
		  if (result < 0 || result > count)
		  {
			throw new System.IndexOutOfRangeException();
		  }
		  return result;
		}

	  }

	  /// <summary>
	  /// A simple IO buffer to use with
	  /// <seealso cref="CharacterUtils#fill(CharacterBuffer, Reader)"/>.
	  /// </summary>
	  public sealed class CharacterBuffer
	  {

		internal readonly char[] buffer;
		internal int offset;
		internal int length;
		// NOTE: not private so outer class can access without
		// $access methods:
		internal char lastTrailingHighSurrogate;

		internal CharacterBuffer(char[] buffer, int offset, int length)
		{
		  this.buffer = buffer;
		  this.offset = offset;
		  this.length = length;
		}

		/// <summary>
		/// Returns the internal buffer
		/// </summary>
		/// <returns> the buffer </returns>
		public char[] Buffer
		{
			get
			{
			  return buffer;
			}
		}

		/// <summary>
		/// Returns the data offset in the internal buffer.
		/// </summary>
		/// <returns> the offset </returns>
		public int Offset
		{
			get
			{
			  return offset;
			}
		}

		/// <summary>
		/// Return the length of the data in the internal buffer starting at
		/// <seealso cref="#getOffset()"/>
		/// </summary>
		/// <returns> the length </returns>
		public int Length
		{
			get
			{
			  return length;
			}
		}

		/// <summary>
		/// Resets the CharacterBuffer. All internals are reset to its default
		/// values.
		/// </summary>
		public void reset()
		{
		  offset = 0;
		  length = 0;
		  lastTrailingHighSurrogate = (char)0;
		}
	  }

	}

}