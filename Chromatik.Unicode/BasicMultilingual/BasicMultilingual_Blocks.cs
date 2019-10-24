using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Chromatik.Unicode
{
    partial class BasicMultilingual
    {
        /// <summary>
        /// <see cref="CodeBlock"/> of the Basic Multilingual plane
        /// </summary>
        public struct BMP
        {
            #region 0000
            /// <summary> </summary>
            static public CodeBlock C0Controls
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "C0 Controls"); }
            }

            /// <summary> </summary>
            static public CodeBlock LatinBasic
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Basic Latin"); }
            }

            /// <summary> </summary>
            static public CodeBlock C1Controls
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "C1 Controls"); }
            }

            /// <summary> </summary>
            static public CodeBlock LatinSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Latin Supplement"); }
            }

            /// <summary> </summary>
            static public CodeBlock LatinExtended_A
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Latin Extended-A"); }
            }

            /// <summary> </summary>
            static public CodeBlock LatinExtended_B
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Latin Extended-B"); }
            }

            /// <summary> </summary>
            static public CodeBlock InternationalPhoneticAlphabet
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "International Phonetic Alphabet"); }
            }

            /// <summary> </summary>
            static public CodeBlock SpacingModifierLetters
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Spacing Modifier Letters"); }
            }

            /// <summary> </summary>
            static public CodeBlock CombiningDiacriticalMarks
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Combining Diacritical Marks"); }
            }

            /// <summary> </summary>
            static public CodeBlock GreekCoptic
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Greek and Coptic"); }
            }

            /// <summary> </summary>
            static public CodeBlock Cyrillic
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Cyrillic"); }
            }

            /// <summary> </summary>
            static public CodeBlock CyrillicSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Cyrillic Supplement"); }
            }

            /// <summary> </summary>
            static public CodeBlock Armenian
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Armenians"); }
            }

            /// <summary> </summary>
            static public CodeBlock Hebrew
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Hebrew"); }
            }

            /// <summary> </summary>
            static public CodeBlock Arabic
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Arabic"); }
            }

            /// <summary> </summary>
            static public CodeBlock Syriac
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Syriac"); }
            }

            /// <summary> </summary>
            static public CodeBlock ArabicSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Arabic Supplement"); }
            }

            /// <summary> </summary>
            static public CodeBlock Thaana
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Thaana"); }
            }

            /// <summary> </summary>
            static public CodeBlock NKo
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "NKo"); }
            }

            /// <summary> </summary>
            static public CodeBlock Samaritan
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Samaritan"); }
            }

            /// <summary> </summary>
            static public CodeBlock Mandaic
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Mandaic"); }
            }

            /// <summary> </summary>
            static public CodeBlock SyriacSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Syriac Supplement"); }
            }

            /// <summary> </summary>
            static public CodeBlock ArabicExtended_A
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Arabic Extended-A"); }
            }

            /// <summary> </summary>
            static public CodeBlock Devanagari
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Devanagari"); }
            }

            /// <summary> </summary>
            static public CodeBlock Bengali
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Gurmukhi"); }
            }

            /// <summary> </summary>
            static public CodeBlock Gurmukhi
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Gurmukhi"); }
            }

            /// <summary> </summary>
            static public CodeBlock Gujarati
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Gujarati"); }
            }

            /// <summary> </summary>
            static public CodeBlock Oriya
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Oriya"); }
            }

            /// <summary> </summary>
            static public CodeBlock Tamil
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Tamil"); }
            }

            /// <summary> </summary>
            static public CodeBlock Telugu
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Telugu"); }
            }

            /// <summary> </summary>
            static public CodeBlock Kannada
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Kannada"); }
            }

            /// <summary> </summary>
            static public CodeBlock Malayalam
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Malayalam"); }
            }

            /// <summary> </summary>
            static public CodeBlock Sinhala
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Sinhala"); }
            }

            /// <summary> </summary>
            static public CodeBlock Thai
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Thai"); }
            }

            /// <summary> </summary>
            static public CodeBlock Lao
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Lao"); }
            }

            /// <summary> </summary>
            static public CodeBlock Tibetan
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Tibetan"); }
            }

            #endregion

            #region 1000
            /// <summary> </summary>
            static public CodeBlock Myanmar
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Myanmar"); }
            }

            /// <summary> </summary>
            static public CodeBlock Georgian
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Georgian"); }
            }

            /// <summary> </summary>
            static public CodeBlock HangulJamo
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Hangul Jamo"); }
            }

            /// <summary> </summary>
            static public CodeBlock Ethiopic
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Ethiopic"); }
            }

            /// <summary> </summary>
            static public CodeBlock EthiopicSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Ethiopic Supplement"); }
            }

            /// <summary> </summary>
            static public CodeBlock Cherokee
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Cherokee"); }
            }

            /// <summary> </summary>
            static public CodeBlock CanadianAboriginalSyllabics
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Unified Canadian Aboriginal Syllabics"); }
            }

            /// <summary> </summary>
            static public CodeBlock Ogham
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Ogham"); }
            }

            /// <summary> </summary>
            static public CodeBlock Runic
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Runic"); }
            }

            /// <summary> </summary>
            static public CodeBlock Tagalog
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Tagalog"); }
            }

            /// <summary> </summary>
            static public CodeBlock Hanunoo
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Hanunoo"); }
            }

            /// <summary> </summary>
            static public CodeBlock Buhid
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Buhid"); }
            }

            /// <summary> </summary>
            static public CodeBlock Tagbanwa
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Tagbanwa"); }
            }

            /// <summary> </summary>
            static public CodeBlock Khmer
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Khmer"); }
            }

            /// <summary> </summary>
            static public CodeBlock Mongolian
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Mongolian"); }
            }

            /// <summary> </summary>
            static public CodeBlock CanadianAboriginalSyllabicsExtended
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Unified Canadian Aboriginal Syllabics Extended"); }
            }

            /// <summary> </summary>
            static public CodeBlock Limbu
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Limbu"); }
            }

            /// <summary> </summary>
            static public CodeBlock TaiLe
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Tai Le"); }
            }

            /// <summary> </summary>
            static public CodeBlock NewTaiLue
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "New Tai Lue"); }
            }

            /// <summary> </summary>
            static public CodeBlock KhmerSymbols
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Khmer Symbols"); }
            }

            /// <summary> </summary>
            static public CodeBlock Buginese
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Buginese"); }
            }

            /// <summary> </summary>
            static public CodeBlock TaiTham
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Tai Tham"); }
            }

            /// <summary> </summary>
            static public CodeBlock CombiningDiacriticalMarksExtended
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Combining Diacritical Marks Extended"); }
            }

            /// <summary> </summary>
            static public CodeBlock Balinese
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Balinese"); }
            }

            /// <summary> </summary>
            static public CodeBlock Sundanese
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Sundanese"); }
            }

            /// <summary> </summary>
            static public CodeBlock Batak
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Batak"); }
            }

            /// <summary> </summary>
            static public CodeBlock Lepcha
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Lepcha"); }
            }

            /// <summary> </summary>
            static public CodeBlock OlChiki
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Ol Chiki"); }
            }

            /// <summary> </summary>
            static public CodeBlock CyrillicExtended_C
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Cyrillic Extended C"); }
            }

            /// <summary> </summary>
            static public CodeBlock SundaneseSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Sundanese Supplement"); }
            }

            /// <summary> </summary>
            static public CodeBlock VedicExtensions
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Vedic Extensions"); }
            }

            /// <summary> </summary>
            static public CodeBlock PhoneticExtensions
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Phonetic Extensions"); }
            }

            /// <summary> </summary>
            static public CodeBlock PhoneticExtensionsSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Phonetic Extensions Supplement"); }
            }

            /// <summary> </summary>
            static public CodeBlock CombiningDiacriticalMarksSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Combining Diacritical Marks Supplement"); }
            }

            /// <summary> </summary>
            static public CodeBlock LatinExtendedAdditional
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Latin Extended Additional"); }
            }

            /// <summary> </summary>
            static public CodeBlock GreekExtended
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Greek Extended"); }
            }
            #endregion

            #region 2000
            /// <summary> </summary>
            static public CodeBlock PunctuationGeneral
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "General Punctuation"); }
            }

            /// <summary> </summary>
            static public CodeBlock SuperscriptsSubscripts
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Superscripts and Subscripts"); }
            }

            /// <summary> </summary>
            static public CodeBlock CurrencySymbols
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Currency Symbols"); }
            }

            /// <summary> </summary>
            static public CodeBlock CombiningDiacriticalMarksSymbols
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Combining Diacritical Marks for Symbols"); }
            }

            /// <summary> </summary>
            static public CodeBlock LetterlikeSymbols
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Letterlike Symbols"); }
            }

            /// <summary> </summary>
            static public CodeBlock NumberForms
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Number Forms"); }
            }

            /// <summary> </summary>
            static public CodeBlock Arrows
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Arrows"); }
            }

            /// <summary> </summary>
            static public CodeBlock MathematicaLOperators
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Mathematical Operators"); }
            }

            /// <summary> </summary>
            static public CodeBlock MiscellaneousTechnical
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Miscellaneous Technical"); }
            }

            /// <summary> </summary>
            static public CodeBlock ControlPictures
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Control Pictures"); }
            }

            /// <summary> </summary>
            static public CodeBlock OpticalCharacterRecognition
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Optical Character Recognition"); }
            }

            /// <summary> </summary>
            static public CodeBlock EnclosedAlphanumerics
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Enclosed Alphanumerics"); }
            }

            /// <summary> </summary>
            static public CodeBlock BoxDrawing
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Box Drawing"); }
            }

            /// <summary> </summary>
            static public CodeBlock BlockElements
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Block Elements"); }
            }

            /// <summary> </summary>
            static public CodeBlock GeometricShapes
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Geometric Shapes"); }
            }

            /// <summary> </summary>
            static public CodeBlock MiscellaneousSymbols
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Miscellaneous Symbols"); }
            }

            /// <summary> </summary>
            static public CodeBlock Dingbats
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Dingbats"); }
            }

            /// <summary> </summary>
            static public CodeBlock MathematicalMiscellaneousSymbols_A
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Miscellaneous Mathematical Symbols-A"); }
            }

            /// <summary> </summary>
            static public CodeBlock ArrowsSupplemental_A
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Supplemental Arrows-A"); }
            }

            /// <summary> </summary>
            static public CodeBlock BraillePatterns
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Braille Patterns"); }
            }

            /// <summary> </summary>
            static public CodeBlock ArrowsSupplemental_B
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Supplemental Arrows-B"); }
            }

            /// <summary> </summary>
            static public CodeBlock MathematicalMiscellaneousSymbols_B
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Miscellaneous Mathematical Symbols-B"); }
            }

            /// <summary> </summary>
            static public CodeBlock MathematicalOperatorsSupplemental
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Mathematical Operators Supplemental"); }
            }

            /// <summary> </summary>
            static public CodeBlock SymbolsArrowsMiscellaneous
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Symbols and Arrows Miscellaneous"); }
            }

            /// <summary> </summary>
            static public CodeBlock Glagolitic
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Glagolitic"); }
            }

            /// <summary> </summary>
            static public CodeBlock LatinExtended_C
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Latin Extended-C"); }
            }

            /// <summary> </summary>
            static public CodeBlock Coptic
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Coptic"); }
            }

            /// <summary> </summary>
            static public CodeBlock GeorgianSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Georgian Supplement"); }
            }

            /// <summary> </summary>
            static public CodeBlock Tifinagh
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Tifinagh"); }
            }

            /// <summary> </summary>
            static public CodeBlock EthiopicExtended
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Ethiopic Extended"); }
            }

            /// <summary> </summary>
            static public CodeBlock CyrillicExtended_A
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Cyrillic Extended-A"); }
            }

            /// <summary> </summary>
            static public CodeBlock PunctuationSupplemental
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Punctuation Supplemental"); }
            }

            /// <summary> </summary>
            static public CodeBlock CJKRadicalsSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "CJK Radicals Supplement"); }
            }

            /// <summary> </summary>
            static public CodeBlock KangxiRadicals
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Kangxi Radicals"); }
            }

            /// <summary> </summary>
            static public CodeBlock IdeographicDescriptionCharacters
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Ideographic Description Characters"); }
            }
            #endregion

            #region 3000
            /// <summary> </summary>
            static public CodeBlock CJKSymbolsPunctuation
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "CJK Symbols and Punctuation"); }
            }

            /// <summary> </summary>
            static public CodeBlock Hiragana
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Hiragan"); }
            }

            /// <summary> </summary>
            static public CodeBlock Katakana
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Katakana"); }
            }

            /// <summary> </summary>
            static public CodeBlock Bopomofo
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Bopomofo"); }
            }

            /// <summary> </summary>
            static public CodeBlock HangulCompatibilityJamo
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Hangul Jamo Compatibility"); }
            }

            /// <summary> </summary>
            static public CodeBlock Kanbun
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Kanbun"); }
            }

            /// <summary> </summary>
            static public CodeBlock BopomofoExtended
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Bopomofo Extended"); }
            }

            /// <summary> </summary>
            static public CodeBlock CJKStrokes
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "CJK Strokes"); }
            }

            /// <summary> </summary>
            static public CodeBlock KatakanaPhoneticExtensions
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Katakana Phonetic Extensions"); }
            }

            /// <summary> </summary>
            static public CodeBlock CJKEnclosedLettersMonths
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "CJK Enclosed Letters and Months"); }
            }

            /// <summary> </summary>
            static public CodeBlock CJKCompatibility
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "CJK Compatibility"); }
            }
            #endregion

            #region 3000-4DBF
            /// <summary> </summary>
            static public CodeBlock CJKUnifiedIdeographsExtension_A
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "CJK Unified Ideographs Extension A"); }
            }
            #endregion

            #region 4000
            /// <summary> </summary>
            static public CodeBlock YijingHexagramSymbols
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Yijing Hexagram Symbols"); }
            }
            #endregion

            #region 4000-9FFFF
            /// <summary> </summary>
            static public CodeBlock CJKUnifiedIdeographs
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "CJK Unified Ideographs"); }
            }
            #endregion

            #region A000
            /// <summary> </summary>
            static public CodeBlock YiSyllables
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Yi Syllables"); }
            }

            /// <summary> </summary>
            static public CodeBlock YiRadicals
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Yi Radicals"); }
            }

            /// <summary> </summary>
            static public CodeBlock Lisu
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Lisu"); }
            }

            /// <summary> </summary>
            static public CodeBlock Vai
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Vai"); }
            }

            /// <summary> </summary>
            static public CodeBlock CyrillicExtended_B
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Cyrillic Extended-B"); }
            }

            /// <summary> </summary>
            static public CodeBlock Bamum
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Bamum"); }
            }

            /// <summary> </summary>
            static public CodeBlock ModifierToneLetters
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Modifier Tone Letters"); }
            }

            /// <summary> </summary>
            static public CodeBlock LatinExtended_D
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Latin Extended-D"); }
            }

            /// <summary> </summary>
            static public CodeBlock SylotiNagri
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Syloti Nagri"); }
            }

            /// <summary> </summary>
            static public CodeBlock IndicNumberForms
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Common Indic Number Forms"); }
            }

            /// <summary> </summary>
            static public CodeBlock Phags_pa
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Phags-pa"); }
            }

            /// <summary> </summary>
            static public CodeBlock Saurashtra
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Saurashtra"); }
            }

            /// <summary> </summary>
            static public CodeBlock DevanagariExtended
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Devanagari Extended"); }
            }

            /// <summary> </summary>
            static public CodeBlock KayahLi
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Kayah Li"); }
            }

            /// <summary> </summary>
            static public CodeBlock Rejang
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Rejang"); }
            }

            /// <summary> </summary>
            static public CodeBlock HangulJamoExtended_A
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Hangul Jamo Extended-A"); }
            }

            /// <summary> </summary>
            static public CodeBlock Javanese
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Javanese"); }
            }

            /// <summary> </summary>
            static public CodeBlock MyanmarExtended_B
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Myanmar Extended-B"); }
            }

            /// <summary> </summary>
            static public CodeBlock Cham
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Cham"); }
            }

            /// <summary> </summary>
            static public CodeBlock MyanmarExtended_A
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Myanmar Extended-A"); }
            }

            /// <summary> </summary>
            static public CodeBlock TaiViet
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Tai Viet"); }
            }

            /// <summary> </summary>
            static public CodeBlock MeeteiMayekExtensions
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Meetei Mayek Extensions"); }
            }

            /// <summary> </summary>
            static public CodeBlock EthiopicExtended_A
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Ethiopic Extended-A"); }
            }

            /// <summary> </summary>
            static public CodeBlock LatinExtended_E
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Latin Extended-E"); }
            }

            /// <summary> </summary>
            static public CodeBlock CherokeeSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Cherokee Supplement"); }
            }

            /// <summary> </summary>
            static public CodeBlock MeeteiMayek
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Meetei Mayek"); }
            }
            #endregion

            #region A000-D7AF
            /// <summary> </summary>
            static public CodeBlock HangulSyllables
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Hangul Syllables"); }
            }
            #endregion

            #region D000
            /// <summary> </summary>
            static public CodeBlock HangulJamoExtended_B
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Hangul Jamo Extended-B"); }
            }
            #endregion

            #region E000-F8FF
            /// <summary> </summary>
            static public CodeBlock PrivateUseArea
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Private Use Area"); }
            }
            #endregion

            #region F000
            /// <summary> </summary>
            static public CodeBlock CJKCompatibilityIdeographs
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "CJK Compatibility Ideographs"); }
            }

            /// <summary> </summary>
            static public CodeBlock AlphabeticPresentationForms
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Alphabetic Presentation Forms"); }
            }

            /// <summary> </summary>
            static public CodeBlock ArabicPresentationForms_A
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Arabic Presentation Forms-A"); }
            }

            /// <summary> </summary>
            static public CodeBlock VariationSelectors
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Variation Selectors"); }
            }

            /// <summary> </summary>
            static public CodeBlock VerticalForms
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Vertical Forms"); }
            }

            /// <summary> </summary>
            static public CodeBlock CombiningHalfMarks
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Combining Half Marks"); }
            }

            /// <summary> </summary>
            static public CodeBlock CJKCompatibilityForms
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "CJK Compatibility Forms"); }
            }

            /// <summary> </summary>
            static public CodeBlock SmallFormVariants
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Small Form Variants"); }
            }

            /// <summary> </summary>
            static public CodeBlock ArabicPresentationForms_B
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Arabic Presentation Forms-B"); }
            }

            /// <summary> </summary>
            static public CodeBlock HalfwidthFullwidthForms
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Halfwidth and Fullwidth Forms"); }
            }

            /// <summary> </summary>
            static public CodeBlock Specials
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Specials"); }
            }
            #endregion

        }
    }
}
