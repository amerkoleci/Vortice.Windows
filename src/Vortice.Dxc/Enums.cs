// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.
// Implementation based on https://github.com/tgjones/DotNetDxc

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Vortice.Dxc
{
    public enum DxcGlobalOptions : uint
    {
        None = 0x0,
        ThreadBackgroundPriorityForIndexing = 0x1,
        ThreadBackgroundPriorityForEditing = 0x2,
        ThreadBackgroundPriorityForAll
    }

    [Flags]
    public enum DxcDiagnosticDisplayOptions : uint
    {
        /// <summary>
        /// Display the source-location information where the diagnostic was located.
        /// </summary>
        DisplaySourceLocation = 0x01,

        /// <summary>
        /// If displaying the source-location information of the diagnostic,
        /// also include the column number.
        /// </summary>
        DisplayColumn = 0x02,

        /// <summary>
        /// If displaying the source-location information of the diagnostic,
        /// also include information about source ranges in a machine-parsable format.
        /// </summary>
        DisplaySourceRanges = 0x04,

        /// <summary>
        /// Display the option name associated with this diagnostic, if any.
        /// </summary>
        DisplayOption = 0x08,

        /// <summary>
        /// Display the category number associated with this diagnostic, if any.
        /// </summary>
        DisplayCategoryId = 0x10,

        /// <summary>
        /// Display the category name associated with this diagnostic, if any.
        /// </summary>
        DisplayCategoryName = 0x20
    }

    public enum DxcDiagnosticSeverity
    {
        /// <summary>
        /// A diagnostic that has been suppressed, e.g., by a command-line option.
        /// </summary>
        Ignored = 0,

        /// <summary>
        /// This diagnostic is a note that should be attached to the previous (non-note) diagnostic.
        /// </summary>
        Note = 1,

        /// <summary>
        /// This diagnostic indicates suspicious code that may not be wrong.
        /// </summary>
        Warning = 2,

        /// <summary>
        /// This diagnostic indicates that the code is ill-formed.
        /// </summary>
        Error = 3,

        /// <summary>
        /// This diagnostic indicates that the code is ill-formed such that future
        /// parser rec unlikely to produce useful results.
        /// </summary>
        Fatal = 4
    }

    [Flags]
    public enum DxcTranslationUnitFlags : uint
    {
        /// <summary>
        /// Used to indicate that no special translation-unit options are needed.
        /// </summary>
        None = 0x0,

        // Used to indicate that the parser should construct a "detailed"
        // preprocessing record, including all macro definitions and instantiations.
        DetailedPreprocessingRecord = 0x01,

        /// <summary>
        /// Used to indicate that the translation unit is incomplete.
        /// </summary>
        Incomplete = 0x02,

        /// <summary>
        /// Used to indicate that the translation unit should be built with an
        /// implicit precompiled header for the preamble.
        /// </summary>
        PrecompiledPreamble = 0x04,

        /// <summary>
        /// Used to indicate that the translation unit should cache some
        /// code-completion results with each reparse of the source file.
        /// </summary>
        CacheCompletionResults = 0x08,

        /// <summary>
        /// Used to indicate that the translation unit will be serialized with
        /// SaveTranslationUnit.
        /// </summary>
        ForSerialization = 0x10,

        /// <summary>
        /// DEPRECATED
        /// </summary>
        CXXChainedPCH = 0x20,

        /// <summary>
        /// Used to indicate that function/method bodies should be skipped while parsing.
        /// </summary>
        SkipFunctionBodies = 0x40,

        /// <summary>
        /// Used to indicate that brief documentation comments should be
        /// included into the set of code completions returned from this translation
        /// unit.
        /// </summary>
        IncludeBriefCommentsInCodeCompletion = 0x80,

        /// <summary>
        /// Used to indicate that compilation should occur on the caller's thread.
        /// </summary>
        UseCallerThread = 0x800,
    }

    [Flags]
    public enum DxcCursorKindFlags : uint
    {
        None = 0,
        Declaration = 0x1,
        Reference = 0x2,
        Expression = 0x4,
        Statement = 0x8,
        Attribute = 0x10,
        Invalid = 0x20,
        TranslationUnit = 0x40,
        Preprocessing = 0x80,
        Unexposed = 0x100,
    }

    /// <summary>
    /// The kind of language construct in a translation unit that a cursor refers to.
    /// </summary>
    public enum DxcCursorKind : uint
    {
        /**
         * \brief A declaration whose specific kind is not exposed via this
         * interface.
         *
         * Unexposed declarations have the same operations as any other kind
         * of declaration; one can extract their location information,
         * spelling, find their definitions, etc. However, the specific kind
         * of the declaration is not reported.
         */
        DxcCursor_UnexposedDecl = 1,
        /** \brief A C or C++ struct. */
        DxcCursor_StructDecl = 2,
        /** \brief A C or C++ union. */
        DxcCursor_UnionDecl = 3,
        /** \brief A C++ class. */
        DxcCursor_ClassDecl = 4,
        /** \brief An enumeration. */
        DxcCursor_EnumDecl = 5,
        /**
         * \brief A field (in C) or non-static data member (in C++) in a
         * struct, union, or C++ class.
         */
        DxcCursor_FieldDecl = 6,
        /** \brief An enumerator constant. */
        DxcCursor_EnumConstantDecl = 7,
        /** \brief A function. */
        DxcCursor_FunctionDecl = 8,
        /** \brief A variable. */
        DxcCursor_VarDecl = 9,
        /** \brief A function or method parameter. */
        DxcCursor_ParmDecl = 10,
        /** \brief An Objective-C \@interface. */
        DxcCursor_ObjCInterfaceDecl = 11,
        /** \brief An Objective-C \@interface for a category. */
        DxcCursor_ObjCCategoryDecl = 12,
        /** \brief An Objective-C \@protocol declaration. */
        DxcCursor_ObjCProtocolDecl = 13,
        /** \brief An Objective-C \@property declaration. */
        DxcCursor_ObjCPropertyDecl = 14,
        /** \brief An Objective-C instance variable. */
        DxcCursor_ObjCIvarDecl = 15,
        /** \brief An Objective-C instance method. */
        DxcCursor_ObjCInstanceMethodDecl = 16,
        /** \brief An Objective-C class method. */
        DxcCursor_ObjCClassMethodDecl = 17,
        /** \brief An Objective-C \@implementation. */
        DxcCursor_ObjCImplementationDecl = 18,
        /** \brief An Objective-C \@implementation for a category. */
        DxcCursor_ObjCCategoryImplDecl = 19,
        /** \brief A typedef */
        DxcCursor_TypedefDecl = 20,
        /** \brief A C++ class method. */
        DxcCursor_CXXMethod = 21,
        /** \brief A C++ namespace. */
        DxcCursor_Namespace = 22,
        /** \brief A linkage specification, e.g. 'extern "C"'. */
        DxcCursor_LinkageSpec = 23,
        /** \brief A C++ constructor. */
        DxcCursor_Constructor = 24,
        /** \brief A C++ destructor. */
        DxcCursor_Destructor = 25,
        /** \brief A C++ conversion function. */
        DxcCursor_ConversionFunction = 26,
        /** \brief A C++ template type parameter. */
        DxcCursor_TemplateTypeParameter = 27,
        /** \brief A C++ non-type template parameter. */
        DxcCursor_NonTypeTemplateParameter = 28,
        /** \brief A C++ template template parameter. */
        DxcCursor_TemplateTemplateParameter = 29,
        /** \brief A C++ function template. */
        DxcCursor_FunctionTemplate = 30,
        /** \brief A C++ class template. */
        DxcCursor_ClassTemplate = 31,
        /** \brief A C++ class template partial specialization. */
        DxcCursor_ClassTemplatePartialSpecialization = 32,
        /** \brief A C++ namespace alias declaration. */
        DxcCursor_NamespaceAlias = 33,
        /** \brief A C++ using directive. */
        DxcCursor_UsingDirective = 34,
        /** \brief A C++ using declaration. */
        DxcCursor_UsingDeclaration = 35,
        /** \brief A C++ alias declaration */
        DxcCursor_TypeAliasDecl = 36,
        /** \brief An Objective-C \@synthesize definition. */
        DxcCursor_ObjCSynthesizeDecl = 37,
        /** \brief An Objective-C \@dynamic definition. */
        DxcCursor_ObjCDynamicDecl = 38,
        /** \brief An access specifier. */
        DxcCursor_CXXAccessSpecifier = 39,

        DxcCursor_FirstDecl = DxcCursor_UnexposedDecl,
        DxcCursor_LastDecl = DxcCursor_CXXAccessSpecifier,

        /* References */
        DxcCursor_FirstRef = 40, /* Decl references */
        DxcCursor_ObjCSuperClassRef = 40,
        DxcCursor_ObjCProtocolRef = 41,
        DxcCursor_ObjCClassRef = 42,
        /**
         * \brief A reference to a type declaration.
         *
         * A type reference occurs anywhere where a type is named but not
         * declared. For example, given:
         *
         * \code
         * typedef unsigned size_type;
         * size_type size;
         * \endcode
         *
         * The typedef is a declaration of size_type (DxcCursor_TypedefDecl),
         * while the type of the variable "size" is referenced. The cursor
         * referenced by the type of size is the typedef for size_type.
         */
        DxcCursor_TypeRef = 43,
        DxcCursor_CXXBaseSpecifier = 44,
        /** 
         * \brief A reference to a class template, function template, template
         * template parameter, or class template partial specialization.
         */
        DxcCursor_TemplateRef = 45,
        /**
         * \brief A reference to a namespace or namespace alias.
         */
        DxcCursor_NamespaceRef = 46,
        /**
         * \brief A reference to a member of a struct, union, or class that occurs in 
         * some non-expression context, e.g., a designated initializer.
         */
        DxcCursor_MemberRef = 47,
        /**
         * \brief A reference to a labeled statement.
         *
         * This cursor kind is used to describe the jump to "start_over" in the 
         * goto statement in the following example:
         *
         * \code
         *   start_over:
         *     ++counter;
         *
         *     goto start_over;
         * \endcode
         *
         * A label reference cursor refers to a label statement.
         */
        DxcCursor_LabelRef = 48,

        /// <summary>
        /// A reference to a set of overloaded functions or function templates
        /// that has not yet been resolved to a specific function or function template.
        /// </summary>
        /// <remarks>
        /// An overloaded declaration reference cursor occurs in C++ templates where
        /// a dependent name refers to a function.
        /// </remarks>
        DxcCursor_OverloadedDeclRef = 49,

        /**
         * \brief A reference to a variable that occurs in some non-expression 
         * context, e.g., a C++ lambda capture list.
         */
        DxcCursor_VariableRef = 50,

        DxcCursor_LastRef = DxcCursor_VariableRef,

        /* Error conditions */
        DxcCursor_FirstInvalid = 70,
        DxcCursor_InvalidFile = 70,
        DxcCursor_NoDeclFound = 71,
        DxcCursor_NotImplemented = 72,
        DxcCursor_InvalidCode = 73,
        DxcCursor_LastInvalid = DxcCursor_InvalidCode,

        /* Expressions */
        DxcCursor_FirstExpr = 100,

        /**
         * \brief An expression whose specific kind is not exposed via this
         * interface.
         *
         * Unexposed expressions have the same operations as any other kind
         * of expression; one can extract their location information,
         * spelling, children, etc. However, the specific kind of the
         * expression is not reported.
         */
        DxcCursor_UnexposedExpr = 100,

        /**
         * \brief An expression that refers to some value declaration, such
         * as a function, varible, or enumerator.
         */
        DxcCursor_DeclRefExpr = 101,

        /**
         * \brief An expression that refers to a member of a struct, union,
         * class, Objective-C class, etc.
         */
        DxcCursor_MemberRefExpr = 102,

        /** \brief An expression that calls a function. */
        DxcCursor_CallExpr = 103,

        /** \brief An expression that sends a message to an Objective-C
         object or class. */
        DxcCursor_ObjCMessageExpr = 104,

        /** \brief An expression that represents a block literal. */
        DxcCursor_BlockExpr = 105,

        /** \brief An integer literal.
         */
        DxcCursor_IntegerLiteral = 106,

        /** \brief A floating point number literal.
         */
        DxcCursor_FloatingLiteral = 107,

        /** \brief An imaginary number literal.
         */
        DxcCursor_ImaginaryLiteral = 108,

        /** \brief A string literal.
         */
        DxcCursor_StringLiteral = 109,

        /** \brief A character literal.
         */
        DxcCursor_CharacterLiteral = 110,

        /** \brief A parenthesized expression, e.g. "(1)".
         *
         * This AST node is only formed if full location information is requested.
         */
        DxcCursor_ParenExpr = 111,

        /** \brief This represents the unary-expression's (except sizeof and
         * alignof).
         */
        DxcCursor_UnaryOperator = 112,

        /** \brief [C99 6.5.2.1] Array Subscripting.
         */
        DxcCursor_ArraySubscriptExpr = 113,

        /** \brief A builtin binary operation expression such as "x + y" or
         * "x <= y".
         */
        DxcCursor_BinaryOperator = 114,

        /** \brief Compound assignment such as "+=".
         */
        DxcCursor_CompoundAssignOperator = 115,

        /** \brief The ?: ternary operator.
         */
        DxcCursor_ConditionalOperator = 116,

        /** \brief An explicit cast in C (C99 6.5.4) or a C-style cast in C++
         * (C++ [expr.cast]), which uses the syntax (Type)expr.
         *
         * For example: (int)f.
         */
        DxcCursor_CStyleCastExpr = 117,

        /** \brief [C99 6.5.2.5]
         */
        DxcCursor_CompoundLiteralExpr = 118,

        /** \brief Describes an C or C++ initializer list.
         */
        DxcCursor_InitListExpr = 119,

        /** \brief The GNU address of label extension, representing &&label.
         */
        DxcCursor_AddrLabelExpr = 120,

        /** \brief This is the GNU Statement Expression extension: ({int X=4; X;})
         */
        DxcCursor_StmtExpr = 121,

        /** \brief Represents a C11 generic selection.
         */
        DxcCursor_GenericSelectionExpr = 122,

        /** \brief Implements the GNU __null extension, which is a name for a null
         * pointer constant that has integral type (e.g., int or long) and is the same
         * size and alignment as a pointer.
         *
         * The __null extension is typically only used by system headers, which define
         * NULL as __null in C++ rather than using 0 (which is an integer that may not
         * match the size of a pointer).
         */
        DxcCursor_GNUNullExpr = 123,

        /** \brief C++'s static_cast<> expression.
         */
        DxcCursor_CXXStaticCastExpr = 124,

        /** \brief C++'s dynamic_cast<> expression.
         */
        DxcCursor_CXXDynamicCastExpr = 125,

        /** \brief C++'s reinterpret_cast<> expression.
         */
        DxcCursor_CXXReinterpretCastExpr = 126,

        /** \brief C++'s const_cast<> expression.
         */
        DxcCursor_CXXConstCastExpr = 127,

        /** \brief Represents an explicit C++ type conversion that uses "functional"
         * notion (C++ [expr.type.conv]).
         *
         * Example:
         * \code
         *   x = int(0.5);
         * \endcode
         */
        DxcCursor_CXXFunctionalCastExpr = 128,

        /** \brief A C++ typeid expression (C++ [expr.typeid]).
         */
        DxcCursor_CXXTypeidExpr = 129,

        /** \brief [C++ 2.13.5] C++ Boolean Literal.
         */
        DxcCursor_CXXBoolLiteralExpr = 130,

        /** \brief [C++0x 2.14.7] C++ Pointer Literal.
         */
        DxcCursor_CXXNullPtrLiteralExpr = 131,

        /** \brief Represents the "this" expression in C++
         */
        DxcCursor_CXXThisExpr = 132,

        /** \brief [C++ 15] C++ Throw Expression.
         *
         * This handles 'throw' and 'throw' assignment-expression. When
         * assignment-expression isn't present, Op will be null.
         */
        DxcCursor_CXXThrowExpr = 133,

        /** \brief A new expression for memory allocation and constructor calls, e.g:
         * "new CXXNewExpr(foo)".
         */
        DxcCursor_CXXNewExpr = 134,

        /** \brief A delete expression for memory deallocation and destructor calls,
         * e.g. "delete[] pArray".
         */
        DxcCursor_CXXDeleteExpr = 135,

        /** \brief A unary expression.
         */
        DxcCursor_UnaryExpr = 136,

        /** \brief An Objective-C string literal i.e. @"foo".
         */
        DxcCursor_ObjCStringLiteral = 137,

        /** \brief An Objective-C \@encode expression.
         */
        DxcCursor_ObjCEncodeExpr = 138,

        /** \brief An Objective-C \@selector expression.
         */
        DxcCursor_ObjCSelectorExpr = 139,

        /** \brief An Objective-C \@protocol expression.
         */
        DxcCursor_ObjCProtocolExpr = 140,

        /** \brief An Objective-C "bridged" cast expression, which casts between
         * Objective-C pointers and C pointers, transferring ownership in the process.
         *
         * \code
         *   NSString *str = (__bridge_transfer NSString *)CFCreateString();
         * \endcode
         */
        DxcCursor_ObjCBridgedCastExpr = 141,

        /** \brief Represents a C++0x pack expansion that produces a sequence of
         * expressions.
         *
         * A pack expansion expression contains a pattern (which itself is an
         * expression) followed by an ellipsis. For example:
         *
         * \code
         * template<typename F, typename ...Types>
         * void forward(F f, Types &&...args) {
         *  f(static_cast<Types&&>(args)...);
         * }
         * \endcode
         */
        DxcCursor_PackExpansionExpr = 142,

        /** \brief Represents an expression that computes the length of a parameter
         * pack.
         *
         * \code
         * template<typename ...Types>
         * struct count {
         *   static const unsigned value = sizeof...(Types);
         * };
         * \endcode
         */
        DxcCursor_SizeOfPackExpr = 143,

        /* \brief Represents a C++ lambda expression that produces a local function
         * object.
         *
         * \code
         * void abssort(float *x, unsigned N) {
         *   std::sort(x, x + N,
         *             [](float a, float b) {
         *               return std::abs(a) < std::abs(b);
         *             });
         * }
         * \endcode
         */
        DxcCursor_LambdaExpr = 144,

        /** \brief Objective-c Boolean Literal.
         */
        DxcCursor_ObjCBoolLiteralExpr = 145,

        /** \brief Represents the "self" expression in a ObjC method.
         */
        DxcCursor_ObjCSelfExpr = 146,

        DxcCursor_LastExpr = DxcCursor_ObjCSelfExpr,

        /* Statements */
        DxcCursor_FirstStmt = 200,
        /**
         * \brief A statement whose specific kind is not exposed via this
         * interface.
         *
         * Unexposed statements have the same operations as any other kind of
         * statement; one can extract their location information, spelling,
         * children, etc. However, the specific kind of the statement is not
         * reported.
         */
        DxcCursor_UnexposedStmt = 200,

        /** \brief A labelled statement in a function. 
         *
         * This cursor kind is used to describe the "start_over:" label statement in 
         * the following example:
         *
         * \code
         *   start_over:
         *     ++counter;
         * \endcode
         *
         */
        DxcCursor_LabelStmt = 201,

        /** \brief A group of statements like { stmt stmt }.
         *
         * This cursor kind is used to describe compound statements, e.g. function
         * bodies.
         */
        DxcCursor_CompoundStmt = 202,

        /** \brief A case statement.
         */
        DxcCursor_CaseStmt = 203,

        /** \brief A default statement.
         */
        DxcCursor_DefaultStmt = 204,

        /** \brief An if statement
         */
        DxcCursor_IfStmt = 205,

        /** \brief A switch statement.
         */
        DxcCursor_SwitchStmt = 206,

        /** \brief A while statement.
         */
        DxcCursor_WhileStmt = 207,

        /** \brief A do statement.
         */
        DxcCursor_DoStmt = 208,

        /** \brief A for statement.
         */
        DxcCursor_ForStmt = 209,

        /** \brief A goto statement.
         */
        DxcCursor_GotoStmt = 210,

        /** \brief An indirect goto statement.
         */
        DxcCursor_IndirectGotoStmt = 211,

        /** \brief A continue statement.
         */
        DxcCursor_ContinueStmt = 212,

        /** \brief A break statement.
         */
        DxcCursor_BreakStmt = 213,

        /** \brief A return statement.
         */
        DxcCursor_ReturnStmt = 214,

        /** \brief A GCC inline assembly statement extension.
         */
        DxcCursor_GCCAsmStmt = 215,
        DxcCursor_AsmStmt = DxcCursor_GCCAsmStmt,

        /** \brief Objective-C's overall \@try-\@catch-\@finally statement.
         */
        DxcCursor_ObjCAtTryStmt = 216,

        /** \brief Objective-C's \@catch statement.
         */
        DxcCursor_ObjCAtCatchStmt = 217,

        /** \brief Objective-C's \@finally statement.
         */
        DxcCursor_ObjCAtFinallyStmt = 218,

        /** \brief Objective-C's \@throw statement.
         */
        DxcCursor_ObjCAtThrowStmt = 219,

        /** \brief Objective-C's \@synchronized statement.
         */
        DxcCursor_ObjCAtSynchronizedStmt = 220,

        /** \brief Objective-C's autorelease pool statement.
         */
        DxcCursor_ObjCAutoreleasePoolStmt = 221,

        /** \brief Objective-C's collection statement.
         */
        DxcCursor_ObjCForCollectionStmt = 222,

        /** \brief C++'s catch statement.
         */
        DxcCursor_CXXCatchStmt = 223,

        /** \brief C++'s try statement.
         */
        DxcCursor_CXXTryStmt = 224,

        /** \brief C++'s for (* : *) statement.
         */
        DxcCursor_CXXForRangeStmt = 225,

        /** \brief Windows Structured Exception Handling's try statement.
         */
        DxcCursor_SEHTryStmt = 226,

        /** \brief Windows Structured Exception Handling's except statement.
         */
        DxcCursor_SEHExceptStmt = 227,

        /** \brief Windows Structured Exception Handling's finally statement.
         */
        DxcCursor_SEHFinallyStmt = 228,

        /** \brief A MS inline assembly statement extension.
         */
        DxcCursor_MSAsmStmt = 229,

        /** \brief The null satement ";": C99 6.8.3p3.
         *
         * This cursor kind is used to describe the null statement.
         */
        DxcCursor_NullStmt = 230,

        /** \brief Adaptor class for mixing declarations with statements and
         * expressions.
         */
        DxcCursor_DeclStmt = 231,

        /** \brief OpenMP parallel directive.
         */
        DxcCursor_OMPParallelDirective = 232,

        DxcCursor_LastStmt = DxcCursor_OMPParallelDirective,

        /**
         * \brief Cursor that represents the translation unit itself.
         *
         * The translation unit cursor exists primarily to act as the root
         * cursor for traversing the contents of a translation unit.
         */
        DxcCursor_TranslationUnit = 300,

        /* Attributes */
        DxcCursor_FirstAttr = 400,
        /**
         * \brief An attribute whose specific kind is not exposed via this
         * interface.
         */
        DxcCursor_UnexposedAttr = 400,

        DxcCursor_IBActionAttr = 401,
        DxcCursor_IBOutletAttr = 402,
        DxcCursor_IBOutletCollectionAttr = 403,
        DxcCursor_CXXFinalAttr = 404,
        DxcCursor_CXXOverrideAttr = 405,
        DxcCursor_AnnotateAttr = 406,
        DxcCursor_AsmLabelAttr = 407,
        DxcCursor_PackedAttr = 408,
        DxcCursor_LastAttr = DxcCursor_PackedAttr,

        /* Preprocessing */
        DxcCursor_PreprocessingDirective = 500,
        DxcCursor_MacroDefinition = 501,
        DxcCursor_MacroExpansion = 502,
        DxcCursor_MacroInstantiation = DxcCursor_MacroExpansion,
        DxcCursor_InclusionDirective = 503,
        DxcCursor_FirstPreprocessing = DxcCursor_PreprocessingDirective,
        DxcCursor_LastPreprocessing = DxcCursor_InclusionDirective,

        /* Extra Declarations */
        /**
         * \brief A module import declaration.
         */
        DxcCursor_ModuleImportDecl = 600,
        DxcCursor_FirstExtraDecl = DxcCursor_ModuleImportDecl,
        DxcCursor_LastExtraDecl = DxcCursor_ModuleImportDecl
    }

    /// <summary>
    /// Describes a kind of token.
    /// </summary>
    public enum DxcTokenKind : uint
    {
        /// <summary>
        /// A token that contains some kind of punctuation.
        /// </summary>
        Punctuation = 0,

        /// <summary>
        /// A language keyword.
        /// </summary>
        Keyword = 1,

        /// <summary>
        /// An identifier (that is not a keyword)
        /// </summary>
        Identifier = 2,

        /// <summary>
        /// A numeric, string, or character literal.
        /// </summary>
        Literal = 3,

        /// <summary>
        /// A comment.
        /// </summary>
        Comment = 4,

        /// <summary>
        /// An unknown token (possibly known to a future version).
        /// </summary>
        Unknown = 5,

        /// <summary>
        /// The token matches a built-in type.
        /// </summary>
        BuiltInType = 6,
    }
}
