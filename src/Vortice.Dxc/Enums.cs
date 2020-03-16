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
        UnexposedDecl = 1,
        /** \brief A C or C++ struct. */
        StructDecl = 2,
        /** \brief A C or C++ union. */
        UnionDecl = 3,
        /** \brief A C++ class. */
        ClassDecl = 4,
        /** \brief An enumeration. */
        EnumDecl = 5,
        /**
         * \brief A field (in C) or non-static data member (in C++) in a
         * struct, union, or C++ class.
         */
        FieldDecl = 6,
        /** \brief An enumerator constant. */
        EnumConstantDecl = 7,
        /** \brief A function. */
        FunctionDecl = 8,
        /** \brief A variable. */
        VarDecl = 9,
        /** \brief A function or method parameter. */
        ParmDecl = 10,
        /** \brief An Objective-C \@interface. */
        ObjCInterfaceDecl = 11,
        /** \brief An Objective-C \@interface for a category. */
        ObjCCategoryDecl = 12,
        /** \brief An Objective-C \@protocol declaration. */
        ObjCProtocolDecl = 13,
        /** \brief An Objective-C \@property declaration. */
        ObjCPropertyDecl = 14,
        /** \brief An Objective-C instance variable. */
        ObjCIvarDecl = 15,
        /** \brief An Objective-C instance method. */
        ObjCInstanceMethodDecl = 16,
        /** \brief An Objective-C class method. */
        ObjCClassMethodDecl = 17,
        /** \brief An Objective-C \@implementation. */
        ObjCImplementationDecl = 18,
        /** \brief An Objective-C \@implementation for a category. */
        ObjCCategoryImplDecl = 19,
        /** \brief A typedef */
        TypedefDecl = 20,
        /** \brief A C++ class method. */
        CXXMethod = 21,
        /** \brief A C++ namespace. */
        Namespace = 22,
        /** \brief A linkage specification, e.g. 'extern "C"'. */
        LinkageSpec = 23,
        /** \brief A C++ constructor. */
        Constructor = 24,
        /** \brief A C++ destructor. */
        Destructor = 25,
        /** \brief A C++ conversion function. */
        ConversionFunction = 26,
        /** \brief A C++ template type parameter. */
        TemplateTypeParameter = 27,
        /** \brief A C++ non-type template parameter. */
        NonTypeTemplateParameter = 28,
        /** \brief A C++ template template parameter. */
        TemplateTemplateParameter = 29,
        /** \brief A C++ function template. */
        FunctionTemplate = 30,
        /** \brief A C++ class template. */
        ClassTemplate = 31,
        /** \brief A C++ class template partial specialization. */
        ClassTemplatePartialSpecialization = 32,
        /** \brief A C++ namespace alias declaration. */
        NamespaceAlias = 33,
        /** \brief A C++ using directive. */
        UsingDirective = 34,
        /** \brief A C++ using declaration. */
        UsingDeclaration = 35,
        /** \brief A C++ alias declaration */
        TypeAliasDecl = 36,
        /** \brief An Objective-C \@synthesize definition. */
        ObjCSynthesizeDecl = 37,
        /** \brief An Objective-C \@dynamic definition. */
        ObjCDynamicDecl = 38,
        /** \brief An access specifier. */
        CXXAccessSpecifier = 39,

        FirstDecl = UnexposedDecl,
        LastDecl = CXXAccessSpecifier,

        /* References */
        FirstRef = 40, /* Decl references */
        ObjCSuperClassRef = 40,
        ObjCProtocolRef = 41,
        ObjCClassRef = 42,
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
         * The typedef is a declaration of size_type (TypedefDecl),
         * while the type of the variable "size" is referenced. The cursor
         * referenced by the type of size is the typedef for size_type.
         */
        TypeRef = 43,
        CXXBaseSpecifier = 44,
        /** 
         * \brief A reference to a class template, function template, template
         * template parameter, or class template partial specialization.
         */
        TemplateRef = 45,
        /**
         * \brief A reference to a namespace or namespace alias.
         */
        NamespaceRef = 46,
        /**
         * \brief A reference to a member of a struct, union, or class that occurs in 
         * some non-expression context, e.g., a designated initializer.
         */
        MemberRef = 47,
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
        LabelRef = 48,

        /// <summary>
        /// A reference to a set of overloaded functions or function templates
        /// that has not yet been resolved to a specific function or function template.
        /// </summary>
        /// <remarks>
        /// An overloaded declaration reference cursor occurs in C++ templates where
        /// a dependent name refers to a function.
        /// </remarks>
        OverloadedDeclRef = 49,

        /// <summary>
        /// A reference to a variable that occurs in some non-expression 
        /// context, e.g., a C++ lambda capture list.
        /// </summary>
        VariableRef = 50,

        LastRef = VariableRef,

        FirstInvalid = 70,
        InvalidFile = 70,
        NoDeclFound = 71,
        NotImplemented = 72,
        InvalidCode = 73,
        LastInvalid = InvalidCode,

        FirstExpr = 100,
        /// <summary>
        /// An expression whose specific kind is not exposed via this
        /// interface.
        /// 
        /// Unexposed expressions have the same operations as any other kind
        /// of expression; one can extract their location information,
        /// spelling, children, etc. However, the specific kind of the
        /// expression is not reported.
        /// </summary>
        UnexposedExpr = 100,

        /// <summary>
        /// brief An expression that refers to some value declaration, such as a function, varible, or enumerator.
        /// </summary>
        DeclRefExpr = 101,

        /// <summary>
        /// An expression that refers to a member of a struct, union, class, Objective-C class, etc.
        /// </summary>
        MemberRefExpr = 102,

        /// <summary>
        /// An expression that calls a function.
        /// </summary>
        CallExpr = 103,

        /// <summary>
        /// An expression that sends a message to an Objective-C object or class.
        /// </summary>
        ObjCMessageExpr = 104,

        /// <summary>
        /// An expression that represents a block literal.
        /// </summary>
        BlockExpr = 105,

        /// <summary>
        /// An integer literal.
        /// </summary>
        IntegerLiteral = 106,

        /// <summary>
        /// A floating point number literal.
        /// </summary>
        FloatingLiteral = 107,

        /// <summary>
        /// An imaginary number literal.
        /// </summary>
        ImaginaryLiteral = 108,

        /// <summary>
        /// A string literal.
        /// </summary>
        StringLiteral = 109,

        /// <summary>
        /// A character literal.
        /// </summary>
        CharacterLiteral = 110,

        /// <summary>
        /// A parenthesized expression, e.g. "(1)".
        /// 
        /// This AST node is only formed if full location information is requested.
        /// </summary>
        ParenExpr = 111,

        /// <summary>
        /// This represents the unary-expression's (except sizeof and alignof).
        /// </summary>
        UnaryOperator = 112,

        /// <summary>
        /// [C99 6.5.2.1] Array Subscripting.
        /// </summary>
        ArraySubscriptExpr = 113,

        /// <summary>
        /// A builtin binary operation expression such as "x + y" or "x &lt;= y".
        /// </summary>
        BinaryOperator = 114,

        /// <summary>
        /// Compound assignment such as "+=".
        /// </summary>
        CompoundAssignOperator = 115,

        /// <summary>
        /// The ?: ternary operator.
        /// </summary>
        ConditionalOperator = 116,

        /// <summary>
        /// An explicit cast in C (C99 6.5.4) or a C-style cast in C++ (C++ [expr.cast]), which uses the syntax (Type)expr.
        /// </summary>
        CStyleCastExpr = 117,

        /// <summary>
        /// [C99 6.5.2.5]
        /// </summary>
        CompoundLiteralExpr = 118,

        /// <summary>
        /// Describes an C or C++ initializer list.
        /// </summary>
        InitListExpr = 119,

        /// <summary>
        /// The GNU address of label extension, representing a label.
        /// </summary>
        AddrLabelExpr = 120,

        /// <summary>
        /// This is the GNU Statement Expression extension: ({int X=4; X;})
        /// </summary>
        StmtExpr = 121,

        /// <summary>
        /// Represents a C11 generic selection.
        /// </summary>
        GenericSelectionExpr = 122,

        /// <summary>
        /// Implements the GNU __null extension, which is a name for a null
        /// pointer constant that has integral type (e.g., int or long) and is the same
        /// size and alignment as a pointer.
        /// 
        /// The __null extension is typically only used by system headers, which define
        /// NULL as __null in C++ rather than using 0 (which is an integer that may not
        /// match the size of a pointer).
        /// </summary>
        GNUNullExpr = 123,

        /// <summary>
        /// C++'s static_cast expression.
        /// </summary>
        CXXStaticCastExpr = 124,

        /// <summary>
        /// C++'s dynamic_cast expression.
        /// </summary>
        CXXDynamicCastExpr = 125,

        /** \brief C++'s reinterpret_cast expression.
         */
        CXXReinterpretCastExpr = 126,

        /// <summary>
        /// C++'s const_cast expression.
        /// </summary>
        CXXConstCastExpr = 127,

        /// <summary>
        /// Represents an explicit C++ type conversion that uses "functional"
        /// notion (C++ [expr.type.conv]).
        /// </summary>
        CXXFunctionalCastExpr = 128,

        /// <summary>
        /// A C++ typeid expression (C++ [expr.typeid]).
        /// </summary>
        CXXTypeidExpr = 129,

        /// <summary>
        /// [C++ 2.13.5] C++ Boolean Literal.
        /// </summary>
        CXXBoolLiteralExpr = 130,

        /// <summary>
        /// [C++0x 2.14.7] C++ Pointer Literal.
        /// </summary>
        CXXNullPtrLiteralExpr = 131,

        /// <summary>
        /// Represents the "this" expression in C++
        /// </summary>
        CXXThisExpr = 132,

        /// <summary>
        /// [C++ 15] C++ Throw Expression.
        /// 
        /// This handles 'throw' and 'throw' assignment-expression. When
        /// assignment-expression isn't present, Op will be null.
        /// </summary>
        CXXThrowExpr = 133,

        /// <summary>
        /// A new expression for memory allocation and constructor calls, e.g:
        /// "new CXXNewExpr(foo)".
        /// </summary>
        CXXNewExpr = 134,

        /// <summary>
        /// A delete expression for memory deallocation and destructor calls,
        /// e.g. "delete[] pArray".
        /// </summary>
        CXXDeleteExpr = 135,

        /// <summary>
        /// A unary expression.
        /// </summary>
        UnaryExpr = 136,

        /// <summary>
        /// An Objective-C string literal i.e. @"foo".
        /// </summary>
        ObjCStringLiteral = 137,

        /// <summary>
        /// An Objective-C \@encode expression.
        /// </summary>
        ObjCEncodeExpr = 138,

        /// <summary>
        /// An Objective-C \@selector expression.
        /// </summary>
        ObjCSelectorExpr = 139,

        /// <summary>
        /// An Objective-C \@protocol expression.
        /// </summary>
        ObjCProtocolExpr = 140,

        /** \brief An Objective-C "bridged" cast expression, which casts between
         * Objective-C pointers and C pointers, transferring ownership in the process.
         *
         * \code
         *   NSString *str = (__bridge_transfer NSString *)CFCreateString();
         * \endcode
         */
        ObjCBridgedCastExpr = 141,

        /// <summary>
        /// Represents a C++0x pack expansion that produces a sequence of expressions.
        /// 
        /// A pack expansion expression contains a pattern (which itself is an
        /// expression) followed by an ellipsis.
        /// </summary>
        PackExpansionExpr = 142,

        /// <summary>
        /// Represents an expression that computes the length of a parameter pack.
        /// </summary>
        SizeOfPackExpr = 143,

        /// <summary>
        /// brief Represents a C++ lambda expression that produces a local function object.
        /// </summary>
        LambdaExpr = 144,

        /// <summary>
        /// Objective-c Boolean Literal.
        /// </summary>
        ObjCBoolLiteralExpr = 145,

        /// <summary>
        /// Represents the "self" expression in a ObjC method.
        /// </summary>
        ObjCSelfExpr = 146,

        LastExpr = ObjCSelfExpr,

        /* Statements */
        FirstStmt = 200,
        /**
         * \brief A statement whose specific kind is not exposed via this
         * interface.
         *
         * Unexposed statements have the same operations as any other kind of
         * statement; one can extract their location information, spelling,
         * children, etc. However, the specific kind of the statement is not
         * reported.
         */
        UnexposedStmt = 200,

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
        LabelStmt = 201,

        /// <summary>
        /// A group of statements like { stmt stmt }.
        /// 
        /// This cursor kind is used to describe compound statements, e.g. function bodies.
        /// </summary>
        CompoundStmt = 202,

        /// <summary>
        /// A case statement.
        /// </summary>
        CaseStmt = 203,

        /// <summary>
        /// A default statement.
        /// </summary>
        DefaultStmt = 204,

        /// <summary>
        /// An if statement
        /// </summary>
        IfStmt = 205,

        /// <summary>
        /// A switch statement.
        /// </summary>
        SwitchStmt = 206,

        /// <summary>
        /// A while statement.
        /// </summary>
        WhileStmt = 207,

        /// <summary>
        /// A do statement.
        /// </summary>
        DoStmt = 208,

        /// <summary>
        /// A for statement.
        /// </summary>
        ForStmt = 209,

        /// <summary>
        /// A goto statement.
        /// </summary>
        GotoStmt = 210,

        /// <summary>
        /// An indirect goto statement.
        /// </summary>
        IndirectGotoStmt = 211,

        /// <summary>
        /// A continue statement.
        /// </summary>
        ContinueStmt = 212,

        /// <summary>
        /// A break statement.
        /// </summary>
        BreakStmt = 213,

        /// <summary>
        /// A return statement.
        /// </summary>
        ReturnStmt = 214,

        /// <summary>
        /// A GCC inline assembly statement extension.
        /// </summary>
        GCCAsmStmt = 215,
        AsmStmt = GCCAsmStmt,

        /// <summary>
        /// Objective-C's overall @try-@catch-@finally statement.
        /// </summary>
        ObjCAtTryStmt = 216,

        /// <summary>
        /// Objective-C's \@catch statement.
        /// </summary>
        ObjCAtCatchStmt = 217,

        /// <summary>
        /// Objective-C's \@finally statement.
        /// </summary>
        ObjCAtFinallyStmt = 218,

        /// <summary>
        /// Objective-C's \@throw statement.
        /// </summary>
        ObjCAtThrowStmt = 219,

        /// <summary>
        /// Objective-C's \@synchronized statement.
        /// </summary>
        ObjCAtSynchronizedStmt = 220,

        /// <summary>
        /// Objective-C's autorelease pool statement.
        /// </summary>
        ObjCAutoreleasePoolStmt = 221,

        /// <summary>
        /// Objective-C's collection statement.
        /// </summary>
        ObjCForCollectionStmt = 222,

        /// <summary>
        /// C++'s catch statement.
        /// </summary>
        CXXCatchStmt = 223,

        /// <summary>
        /// C++'s try statement.
        /// </summary>
        CXXTryStmt = 224,

        /// <summary>
        /// C++'s for (* : *) statement.
        /// </summary>
        CXXForRangeStmt = 225,

        /// <summary>
        /// Windows Structured Exception Handling's try statement.
        /// </summary>
        SEHTryStmt = 226,

        /// <summary>
        /// Windows Structured Exception Handling's except statement.
        /// </summary>
        SEHExceptStmt = 227,

        /// <summary>
        /// Windows Structured Exception Handling's finally statement.
        /// </summary>
        SEHFinallyStmt = 228,

        /// <summary>
        /// A MS inline assembly statement extension.
        /// </summary>
        MSAsmStmt = 229,

        /// <summary>
        /// The null satement ";": C99 6.8.3p3.
        /// 
        /// This cursor kind is used to describe the null statement.
        /// </summary>
        NullStmt = 230,

        /// <summary>
        /// Adaptor class for mixing declarations with statements and
        /// expressions.
        /// </summary>
        DeclStmt = 231,

        /// <summary>
        /// OpenMP parallel directive.
        /// </summary>
        OMPParallelDirective = 232,

        LastStmt = OMPParallelDirective,

        /// <summary>
        /// Cursor that represents the translation unit itself.
        /// 
        /// The translation unit cursor exists primarily to act as the root
        /// cursor for traversing the contents of a translation unit.
        /// </summary>
        TranslationUnit = 300,

        /* Attributes */
        FirstAttr = 400,
        /// <summary>
        /// An attribute whose specific kind is not exposed via this interface.
        /// </summary>
        UnexposedAttr = 400,

        IBActionAttr = 401,
        IBOutletAttr = 402,
        IBOutletCollectionAttr = 403,
        CXXFinalAttr = 404,
        CXXOverrideAttr = 405,
        AnnotateAttr = 406,
        AsmLabelAttr = 407,
        PackedAttr = 408,
        LastAttr = PackedAttr,

        PreprocessingDirective = 500,
        MacroDefinition = 501,
        MacroExpansion = 502,
        MacroInstantiation = MacroExpansion,
        InclusionDirective = 503,
        FirstPreprocessing = PreprocessingDirective,
        LastPreprocessing = InclusionDirective,

        /// <summary>
        /// A module import declaration.
        /// </summary>
        ModuleImportDecl = 600,
        FirstExtraDecl = ModuleImportDecl,
        LastExtraDecl = ModuleImportDecl
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
