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
            static public CodeBlock C0Controls
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "C0 Controls"); }
            }

            static public CodeBlock LatinBasic
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Basic Latin"); }
            }

            static public CodeBlock C1Controls
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "C1 Controls"); }
            }

            static public CodeBlock LatinSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Latin Supplement"); }
            }

            static public CodeBlock LatinExtended_A
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Latin Extended-A"); }
            }

            static public CodeBlock LatinExtended_B
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Latin Extended-B"); }
            }

            static public CodeBlock InternationalPhoneticAlphabet
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "International Phonetic Alphabet"); }
            }

            static public CodeBlock SpacingModifierLetters
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Spacing Modifier Letters"); }
            }

            static public CodeBlock CombiningDiacriticalMarks
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Combining Diacritical Marks"); }
            }

            static public CodeBlock GreekCoptic
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Greek and Coptic"); }
            }

            static public CodeBlock Cyrillic
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Cyrillic"); }
            }

            static public CodeBlock CyrillicSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Cyrillic Supplement"); }
            }

            static public CodeBlock Armenian
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Armenians"); }
            }

            static public CodeBlock Hebrew
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Hebrew"); }
            }

            static public CodeBlock Arabic
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Arabic"); }
            }

            static public CodeBlock Syriac
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Syriac"); }
            }

            static public CodeBlock ArabicSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Arabic Supplement"); }
            }

            static public CodeBlock Thaana
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Thaana"); }
            }

            static public CodeBlock NKo
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "NKo"); }
            }

            static public CodeBlock Samaritan
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Samaritan"); }
            }

            static public CodeBlock Mandaic
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Mandaic"); }
            }

            static public CodeBlock SyriacSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Syriac Supplement"); }
            }

            static public CodeBlock ArabicExtended_A
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Arabic Extended-A"); }
            }

            static public CodeBlock Devanagari
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Devanagari"); }
            }

            static public CodeBlock Bengali
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Gurmukhi"); }
            }

            static public CodeBlock Gurmukhi
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Gurmukhi"); }
            }

            static public CodeBlock Gujarati
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Gujarati"); }
            }

            static public CodeBlock Oriya
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Oriya"); }
            }

            static public CodeBlock Tamil
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Tamil"); }
            }

            static public CodeBlock Telugu
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Telugu"); }
            }

            static public CodeBlock Kannada
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Kannada"); }
            }

            static public CodeBlock Malayalam
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Malayalam"); }
            }

            static public CodeBlock Sinhala
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Sinhala"); }
            }

            static public CodeBlock Thai
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Thai"); }
            }

            static public CodeBlock Lao
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Lao"); }
            }

            static public CodeBlock Tibetan
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Tibetan"); }
            }

            #endregion

            #region 1000

            static public CodeBlock Myanmar
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Myanmar"); }
            }

            static public CodeBlock Georgian
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Georgian"); }
            }

            static public CodeBlock HangulJamo
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Hangul Jamo"); }
            }

            static public CodeBlock Ethiopic
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Ethiopic"); }
            }

            static public CodeBlock EthiopicSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Ethiopic Supplement"); }
            }

            static public CodeBlock Cherokee
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Cherokee"); }
            }

            static public CodeBlock CanadianAboriginalSyllabics
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Unified Canadian Aboriginal Syllabics"); }
            }

            static public CodeBlock Ogham
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Ogham"); }
            }

            static public CodeBlock Runic
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Runic"); }
            }

            static public CodeBlock Tagalog
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Tagalog"); }
            }

            static public CodeBlock Hanunoo
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Hanunoo"); }
            }

            static public CodeBlock Buhid
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Buhid"); }
            }

            static public CodeBlock Tagbanwa
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Tagbanwa"); }
            }

            static public CodeBlock Khmer
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Khmer"); }
            }

            static public CodeBlock Mongolian
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Mongolian"); }
            }

            static public CodeBlock CanadianAboriginalSyllabicsExtended
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Unified Canadian Aboriginal Syllabics Extended"); }
            }

            static public CodeBlock Limbu
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Limbu"); }
            }

            static public CodeBlock TaiLe
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Tai Le"); }
            }

            static public CodeBlock NewTaiLue
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "New Tai Lue"); }
            }

            static public CodeBlock KhmerSymbols
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Khmer Symbols"); }
            }

            static public CodeBlock Buginese
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Buginese"); }
            }

            static public CodeBlock TaiTham
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Tai Tham"); }
            }

            static public CodeBlock CombiningDiacriticalMarksExtended
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Combining Diacritical Marks Extended"); }
            }

            static public CodeBlock Balinese
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Balinese"); }
            }

            static public CodeBlock Sundanese
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Sundanese"); }
            }

            static public CodeBlock Batak
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Batak"); }
            }

            static public CodeBlock Lepcha
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Lepcha"); }
            }

            static public CodeBlock OlChiki
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Ol Chiki"); }
            }

            static public CodeBlock CyrillicExtended_C
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Cyrillic Extended C"); }
            }

            static public CodeBlock SundaneseSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Sundanese Supplement"); }
            }

            static public CodeBlock VedicExtensions
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Vedic Extensions"); }
            }

            static public CodeBlock PhoneticExtensions
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Phonetic Extensions"); }
            }

            static public CodeBlock PhoneticExtensionsSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Phonetic Extensions Supplement"); }
            }

            static public CodeBlock CombiningDiacriticalMarksSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Combining Diacritical Marks Supplement"); }
            }

            static public CodeBlock LatinExtendedAdditional
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Latin Extended Additional"); }
            }

            static public CodeBlock GreekExtended
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Greek Extended"); }
            }

            #endregion

            #region 2000

            static public CodeBlock PunctuationGeneral
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "General Punctuation"); }
            }

            static public CodeBlock SuperscriptsSubscripts
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Superscripts and Subscripts"); }
            }

            static public CodeBlock CurrencySymbols
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Currency Symbols"); }
            }

            static public CodeBlock CombiningDiacriticalMarksSymbols
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Combining Diacritical Marks for Symbols"); }
            }

            static public CodeBlock LetterlikeSymbols
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Letterlike Symbols"); }
            }

            static public CodeBlock NumberForms
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Number Forms"); }
            }

            static public CodeBlock Arrows
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Arrows"); }
            }

            static public CodeBlock MathematicaLOperators
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Mathematical Operators"); }
            }

            static public CodeBlock MiscellaneousTechnical
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Miscellaneous Technical"); }
            }

            static public CodeBlock ControlPictures
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Control Pictures"); }
            }

            static public CodeBlock OpticalCharacterRecognition
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Optical Character Recognition"); }
            }

            static public CodeBlock EnclosedAlphanumerics
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Enclosed Alphanumerics"); }
            }

            static public CodeBlock BoxDrawing
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Box Drawing"); }
            }

            static public CodeBlock BlockElements
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Block Elements"); }
            }

            static public CodeBlock GeometricShapes
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Geometric Shapes"); }
            }

            static public CodeBlock MiscellaneousSymbols
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Miscellaneous Symbols"); }
            }

            static public CodeBlock Dingbats
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Dingbats"); }
            }

            static public CodeBlock MathematicalMiscellaneousSymbols_A
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Miscellaneous Mathematical Symbols-A"); }
            }

            static public CodeBlock ArrowsSupplemental_A
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Supplemental Arrows-A"); }
            }

            static public CodeBlock BraillePatterns
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Braille Patterns"); }
            }

            static public CodeBlock ArrowsSupplemental_B
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Supplemental Arrows-B"); }
            }

            static public CodeBlock MathematicalMiscellaneousSymbols_B
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Miscellaneous Mathematical Symbols-B"); }
            }

            static public CodeBlock MathematicalOperatorsSupplemental
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Mathematical Operators Supplemental"); }
            }

            static public CodeBlock SymbolsArrowsMiscellaneous
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Symbols and Arrows Miscellaneous"); }
            }

            static public CodeBlock Glagolitic
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Glagolitic"); }
            }

            static public CodeBlock LatinExtended_C
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Latin Extended-C"); }
            }

            static public CodeBlock Coptic
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Coptic"); }
            }

            static public CodeBlock GeorgianSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Georgian Supplement"); }
            }

            static public CodeBlock Tifinagh
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Tifinagh"); }
            }

            static public CodeBlock EthiopicExtended
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Ethiopic Extended"); }
            }

            static public CodeBlock CyrillicExtended_A
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Cyrillic Extended-A"); }
            }

            static public CodeBlock PunctuationSupplemental
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Punctuation Supplemental"); }
            }

            static public CodeBlock CJKRadicalsSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "CJK Radicals Supplement"); }
            }

            static public CodeBlock KangxiRadicals
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Kangxi Radicals"); }
            }

            static public CodeBlock IdeographicDescriptionCharacters
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Ideographic Description Characters"); }
            }

            #endregion

            #region 3000

            static public CodeBlock CJKSymbolsPunctuation
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "CJK Symbols and Punctuation"); }
            }

            static public CodeBlock Hiragana
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Hiragan"); }
            }

            static public CodeBlock Katakana
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Katakana"); }
            }

            static public CodeBlock Bopomofo
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Bopomofo"); }
            }

            static public CodeBlock HangulCompatibilityJamo
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Hangul Jamo Compatibility"); }
            }

            static public CodeBlock Kanbun
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Kanbun"); }
            }

            static public CodeBlock BopomofoExtended
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Bopomofo Extended"); }
            }

            static public CodeBlock CJKStrokes
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "CJK Strokes"); }
            }

            static public CodeBlock KatakanaPhoneticExtensions
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Katakana Phonetic Extensions"); }
            }

            static public CodeBlock CJKEnclosedLettersMonths
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "CJK Enclosed Letters and Months"); }
            }

            static public CodeBlock CJKCompatibility
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "CJK Compatibility"); }
            }

            #endregion

            #region 3000-4DBF
            static public CodeBlock CJKUnifiedIdeographsExtension_A
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "CJK Unified Ideographs Extension A"); }
            }
            #endregion

            #region 4000

            static public CodeBlock YijingHexagramSymbols
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Yijing Hexagram Symbols"); }
            }

            #endregion

            #region 4000-9FFFF
            static public CodeBlock CJKUnifiedIdeographs
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "CJK Unified Ideographs"); }
            }
            #endregion

            #region A000

            static public CodeBlock YiSyllables
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Yi Syllables"); }
            }

            static public CodeBlock YiRadicals
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Yi Radicals"); }
            }

            static public CodeBlock Lisu
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Lisu"); }
            }

            static public CodeBlock Vai
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Vai"); }
            }

            static public CodeBlock CyrillicExtended_B
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Cyrillic Extended-B"); }
            }

            static public CodeBlock Bamum
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Bamum"); }
            }

            static public CodeBlock ModifierToneLetters
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Modifier Tone Letters"); }
            }

            static public CodeBlock LatinExtended_D
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Latin Extended-D"); }
            }

            static public CodeBlock SylotiNagri
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Syloti Nagri"); }
            }

            static public CodeBlock IndicNumberForms
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Common Indic Number Forms"); }
            }

            static public CodeBlock Phags_pa
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Phags-pa"); }
            }

            static public CodeBlock Saurashtra
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Saurashtra"); }
            }

            static public CodeBlock DevanagariExtended
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Devanagari Extended"); }
            }

            static public CodeBlock KayahLi
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Kayah Li"); }
            }

            static public CodeBlock Rejang
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Rejang"); }
            }

            static public CodeBlock HangulJamoExtended_A
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Hangul Jamo Extended-A"); }
            }

            static public CodeBlock Javanese
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Javanese"); }
            }

            static public CodeBlock MyanmarExtended_B
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Myanmar Extended-B"); }
            }

            static public CodeBlock Cham
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Cham"); }
            }

            static public CodeBlock MyanmarExtended_A
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Myanmar Extended-A"); }
            }

            static public CodeBlock TaiViet
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Tai Viet"); }
            }

            static public CodeBlock MeeteiMayekExtensions
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Meetei Mayek Extensions"); }
            }

            static public CodeBlock EthiopicExtended_A
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Ethiopic Extended-A"); }
            }

            static public CodeBlock LatinExtended_E
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Latin Extended-E"); }
            }

            static public CodeBlock CherokeeSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Cherokee Supplement"); }
            }

            static public CodeBlock MeeteiMayek
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Meetei Mayek"); }
            }

            #endregion

            #region A000-D7AF
            static public CodeBlock HangulSyllables
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Hangul Syllables"); }
            }
            #endregion

            #region D000
            static public CodeBlock HangulJamoExtended_B
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Hangul Jamo Extended-B"); }
            }
            #endregion

            #region E000-F8FF
            static public CodeBlock PrivateUseArea
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Private Use Area"); }
            }
            #endregion

            #region F000

            static public CodeBlock CJKCompatibilityIdeographs
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "CJK Compatibility Ideographs"); }
            }

            static public CodeBlock AlphabeticPresentationForms
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Alphabetic Presentation Forms"); }
            }

            static public CodeBlock ArabicPresentationForms_A
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Arabic Presentation Forms-A"); }
            }

            static public CodeBlock VariationSelectors
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Variation Selectors"); }
            }

            static public CodeBlock VerticalForms
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Vertical Forms"); }
            }

            static public CodeBlock CombiningHalfMarks
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Combining Half Marks"); }
            }

            static public CodeBlock CJKCompatibilityForms
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "CJK Compatibility Forms"); }
            }

            static public CodeBlock SmallFormVariants
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Small Form Variants"); }
            }

            static public CodeBlock ArabicPresentationForms_B
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Arabic Presentation Forms-B"); }
            }

            static public CodeBlock HalfwidthFullwidthForms
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Halfwidth and Fullwidth Forms"); }
            }

            static public CodeBlock Specials
            {
                get { return CodeBlock.LoadFromXml(XmlBMP, XmlLang, "Specials"); }
            }

            #endregion

        }
    }
}
