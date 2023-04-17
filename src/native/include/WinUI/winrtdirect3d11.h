/* Header file automatically generated from winrtdirect3d11.idl */
/*
 * File built with Microsoft(R) MIDLRT Compiler Engine Version 10.00.0231 
 */

#pragma warning( disable: 4049 )  /* more than 64k source lines */

/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 500
#endif

/* verify that the <rpcsal.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCSAL_H_VERSION__
#define __REQUIRED_RPCSAL_H_VERSION__ 100
#endif

#include <rpc.h>
#include <rpcndr.h>

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif /* __RPCNDR_H_VERSION__ */

#ifndef COM_NO_WINDOWS_H
#include <windows.h>
#include <ole2.h>
#endif /*COM_NO_WINDOWS_H*/
#ifndef __winrtdirect3d11_h__
#define __winrtdirect3d11_h__
#ifndef __winrtdirect3d11_p_h__
#define __winrtdirect3d11_p_h__


#pragma once

// Ensure that the setting of the /ns_prefix command line switch is consistent for all headers.
// If you get an error from the compiler indicating "warning C4005: 'CHECK_NS_PREFIX_STATE': macro redefinition", this
// indicates that you have included two different headers with different settings for the /ns_prefix MIDL command line switch
#if !defined(DISABLE_NS_PREFIX_CHECKS)
#if defined(MIDL_NS_PREFIX)
#define CHECK_NS_PREFIX_STATE "always"
#else
#define CHECK_NS_PREFIX_STATE "never"
#endif // MIDL_NS_PREFIX
#endif // !defined(DISABLE_NS_PREFIX_CHECKS)


#pragma push_macro("ABI_CONCAT")
#pragma push_macro("ABI_PARAMETER")
#pragma push_macro("ABI_NAMESPACE_BEGIN")
#pragma push_macro("ABI_NAMESPACE_END")
#pragma push_macro("C_IID")
#undef ABI_CONCAT
#undef ABI_PARAMETER
#undef ABI_NAMESPACE_BEGIN
#undef ABI_NAMESPACE_END
#undef C_IID
#define ABI_CONCAT(x,y)  x##y

// /ns_prefix optional state
#if defined(MIDL_NS_PREFIX)
#if defined(__cplusplus) && !defined(CINTERFACE)
#define ABI_PARAMETER(x) ABI::x
#define ABI_NAMESPACE_BEGIN namespace ABI {
#define ABI_NAMESPACE_END }
#else // !defined(__cplusplus) || defined(CINTERFACE)
#define C_ABI_PARAMETER(x) ABI_CONCAT(__x_ABI_C, x)
#endif // !defined(__cplusplus)
#define C_IID(x) ABI_CONCAT(IID___x_ABI_C, x)
#else
#if defined(__cplusplus) && !defined(CINTERFACE)
#define ABI_PARAMETER(x) x
#define ABI_NAMESPACE_BEGIN 
#define ABI_NAMESPACE_END 
#else // !defined(__cplusplus) || defined(CINTERFACE)
#define C_ABI_PARAMETER(x) ABI_CONCAT(__x_, x)
#endif // !defined(__cplusplus)
#define C_IID(x) ABI_CONCAT(IID___x_, x)
#endif // defined(MIDL_NS_PREFIX)

#pragma push_macro("MIDL_CONST_ID")
#undef MIDL_CONST_ID
#define MIDL_CONST_ID const __declspec(selectany)


//  API Contract Inclusion Definitions
#if !defined(SPECIFIC_API_CONTRACT_DEFINITIONS)
#if !defined(WINDOWS_APPLICATIONMODEL_CALLS_CALLSPHONECONTRACT_VERSION)
#define WINDOWS_APPLICATIONMODEL_CALLS_CALLSPHONECONTRACT_VERSION 0x70000
#endif // defined(WINDOWS_APPLICATIONMODEL_CALLS_CALLSPHONECONTRACT_VERSION)

#if !defined(WINDOWS_FOUNDATION_FOUNDATIONCONTRACT_VERSION)
#define WINDOWS_FOUNDATION_FOUNDATIONCONTRACT_VERSION 0x40000
#endif // defined(WINDOWS_FOUNDATION_FOUNDATIONCONTRACT_VERSION)

#if !defined(WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION)
#define WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION 0xf0000
#endif // defined(WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION)

#if !defined(WINDOWS_NETWORKING_SOCKETS_CONTROLCHANNELTRIGGERCONTRACT_VERSION)
#define WINDOWS_NETWORKING_SOCKETS_CONTROLCHANNELTRIGGERCONTRACT_VERSION 0x30000
#endif // defined(WINDOWS_NETWORKING_SOCKETS_CONTROLCHANNELTRIGGERCONTRACT_VERSION)

#if !defined(WINDOWS_PHONE_PHONECONTRACT_VERSION)
#define WINDOWS_PHONE_PHONECONTRACT_VERSION 0x10000
#endif // defined(WINDOWS_PHONE_PHONECONTRACT_VERSION)

#if !defined(WINDOWS_PHONE_PHONEINTERNALCONTRACT_VERSION)
#define WINDOWS_PHONE_PHONEINTERNALCONTRACT_VERSION 0x10000
#endif // defined(WINDOWS_PHONE_PHONEINTERNALCONTRACT_VERSION)

#if !defined(WINDOWS_UI_WEBUI_CORE_WEBUICOMMANDBARCONTRACT_VERSION)
#define WINDOWS_UI_WEBUI_CORE_WEBUICOMMANDBARCONTRACT_VERSION 0x10000
#endif // defined(WINDOWS_UI_WEBUI_CORE_WEBUICOMMANDBARCONTRACT_VERSION)

#endif // defined(SPECIFIC_API_CONTRACT_DEFINITIONS)


// Header files for imported files
#include "inspectable.h"
#include "Windows.Foundation.h"
#include "WinRTDirectXCommon.h"

#if defined(__cplusplus) && !defined(CINTERFACE)
/* Forward Declarations */
#ifndef ____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice_FWD_DEFINED__
#define ____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice_FWD_DEFINED__
ABI_NAMESPACE_BEGIN 
namespace Windows {
    namespace Graphics {
        namespace DirectX {
            namespace Direct3D11 {
                interface IDirect3DDevice;
            } /* Direct3D11 */
        } /* DirectX */
    } /* Graphics */
} /* Windows */
ABI_NAMESPACE_END
#define __x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice ABI_PARAMETER(Windows::Graphics::DirectX::Direct3D11::IDirect3DDevice)

#endif // ____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice_FWD_DEFINED__

#ifndef ____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface_FWD_DEFINED__
#define ____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface_FWD_DEFINED__
ABI_NAMESPACE_BEGIN 
namespace Windows {
    namespace Graphics {
        namespace DirectX {
            namespace Direct3D11 {
                interface IDirect3DSurface;
            } /* Direct3D11 */
        } /* DirectX */
    } /* Graphics */
} /* Windows */
ABI_NAMESPACE_END
#define __x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface ABI_PARAMETER(Windows::Graphics::DirectX::Direct3D11::IDirect3DSurface)

#endif // ____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface_FWD_DEFINED__



#pragma warning (push)
#pragma warning (disable:4668) 
#pragma warning (disable:4001) 
#pragma once 
#pragma warning (pop)



/*
 *
 * Typedef of Windows.Graphics.DirectX.Direct3D11.Direct3DMultisampleDescription
 *
 * Introduced to Windows.Foundation.UniversalApiContract in version 1.0
 *
 *
 */
#if WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000
ABI_NAMESPACE_BEGIN 
namespace Windows {
    namespace Graphics {
        namespace DirectX {
            namespace Direct3D11 {
                /* [contract, version, version] */
                typedef 
                struct Direct3DMultisampleDescription
                {
                    INT32 Count;
                    INT32 Quality;
                } Direct3DMultisampleDescription;
                
            } /* Direct3D11 */
        } /* DirectX */
    } /* Graphics */
} /* Windows */
ABI_NAMESPACE_END
#endif // WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000


/*
 *
 * Typedef of Windows.Graphics.DirectX.Direct3D11.Direct3DSurfaceDescription
 *
 * Introduced to Windows.Foundation.UniversalApiContract in version 1.0
 *
 *
 */
#if WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000
ABI_NAMESPACE_BEGIN 
namespace Windows {
    namespace Graphics {
        namespace DirectX {
            namespace Direct3D11 {
                /* [contract, version, version] */
                typedef 
                struct Direct3DSurfaceDescription
                {
                    INT32 Width;
                    INT32 Height;
                    ABI_PARAMETER(Windows::Graphics::DirectX::DirectXPixelFormat) Format;
                    ABI_PARAMETER(Windows::Graphics::DirectX::Direct3D11::Direct3DMultisampleDescription) MultisampleDescription;
                } Direct3DSurfaceDescription;
                
            } /* Direct3D11 */
        } /* DirectX */
    } /* Graphics */
} /* Windows */
ABI_NAMESPACE_END
#endif // WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000


/*
 *
 * Typedef of Windows.Graphics.DirectX.Direct3D11.Direct3DUsage
 *
 * Introduced to Windows.Foundation.UniversalApiContract in version 1.0
 *
 *
 */
#if WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000
ABI_NAMESPACE_BEGIN 
namespace Windows {
    namespace Graphics {
        namespace DirectX {
            namespace Direct3D11 {
                /* [contract, version, version] */
                typedef /* [v1_enum] */
                enum Direct3DUsage : int
                {
                    Direct3DUsage_Default = 0,
                    Direct3DUsage_Immutable = 1,
                    Direct3DUsage_Dynamic = 2,
                    Direct3DUsage_Staging = 3,
                } Direct3DUsage;
                
            } /* Direct3D11 */
        } /* DirectX */
    } /* Graphics */
} /* Windows */
ABI_NAMESPACE_END
#endif // WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000


/*
 *
 * Typedef of Windows.Graphics.DirectX.Direct3D11.Direct3DBindings
 *
 * Introduced to Windows.Foundation.UniversalApiContract in version 1.0
 *
 *
 */
#if WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000
ABI_NAMESPACE_BEGIN 
namespace Windows {
    namespace Graphics {
        namespace DirectX {
            namespace Direct3D11 {
                /* [contract, flags, version, version] */
                typedef /* [v1_enum] */
                enum Direct3DBindings : unsigned int
                {
                    Direct3DBindings_VertexBuffer = 0x1,
                    Direct3DBindings_IndexBuffer = 0x2,
                    Direct3DBindings_ConstantBuffer = 0x4,
                    Direct3DBindings_ShaderResource = 0x8,
                    Direct3DBindings_StreamOutput = 0x10,
                    Direct3DBindings_RenderTarget = 0x20,
                    Direct3DBindings_DepthStencil = 0x40,
                    Direct3DBindings_UnorderedAccess = 0x80,
                    Direct3DBindings_Decoder = 0x200,
                    Direct3DBindings_VideoEncoder = 0x400,
                } Direct3DBindings;
                
                DEFINE_ENUM_FLAG_OPERATORS(Direct3DBindings)
                
            } /* Direct3D11 */
        } /* DirectX */
    } /* Graphics */
} /* Windows */
ABI_NAMESPACE_END
#endif // WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000


/*
 *
 * Interface Windows.Graphics.DirectX.Direct3D11.IDirect3DDevice
 *
 * Introduced to Windows.Foundation.UniversalApiContract in version 1.0
 *
 *
 * Any object which implements this interface must also implement the following interfaces:
 *     Windows.Foundation.IClosable
 *
 *
 */
#if WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000
#if !defined(____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice_INTERFACE_DEFINED__)
#define ____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice_INTERFACE_DEFINED__
extern const __declspec(selectany) _Null_terminated_ WCHAR InterfaceName_Windows_Graphics_DirectX_Direct3D11_IDirect3DDevice[] = L"Windows.Graphics.DirectX.Direct3D11.IDirect3DDevice";
ABI_NAMESPACE_BEGIN 
namespace Windows {
    namespace Graphics {
        namespace DirectX {
            namespace Direct3D11 {
                /* [object, contract, uuid("A37624AB-8D5F-4650-9D3E-9EAE3D9BC670"), version, version] */
                MIDL_INTERFACE("A37624AB-8D5F-4650-9D3E-9EAE3D9BC670")
                IDirect3DDevice : public IInspectable
                {
                public:
                    virtual HRESULT STDMETHODCALLTYPE Trim(void) = 0;
                    
                };

                MIDL_CONST_ID IID & IID_IDirect3DDevice=__uuidof(IDirect3DDevice);
                
            } /* Direct3D11 */
        } /* DirectX */
    } /* Graphics */
} /* Windows */
ABI_NAMESPACE_END

EXTERN_C const IID C_IID(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice);
#endif /* !defined(____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice_INTERFACE_DEFINED__) */
#endif // WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000


/*
 *
 * Interface Windows.Graphics.DirectX.Direct3D11.IDirect3DSurface
 *
 * Introduced to Windows.Foundation.UniversalApiContract in version 1.0
 *
 *
 * Any object which implements this interface must also implement the following interfaces:
 *     Windows.Foundation.IClosable
 *
 *
 */
#if WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000
#if !defined(____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface_INTERFACE_DEFINED__)
#define ____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface_INTERFACE_DEFINED__
extern const __declspec(selectany) _Null_terminated_ WCHAR InterfaceName_Windows_Graphics_DirectX_Direct3D11_IDirect3DSurface[] = L"Windows.Graphics.DirectX.Direct3D11.IDirect3DSurface";
ABI_NAMESPACE_BEGIN 
namespace Windows {
    namespace Graphics {
        namespace DirectX {
            namespace Direct3D11 {
                /* [object, contract, uuid("0BF4A146-13C1-4694-BEE3-7ABF15EAF586"), version, version] */
                MIDL_INTERFACE("0BF4A146-13C1-4694-BEE3-7ABF15EAF586")
                IDirect3DSurface : public IInspectable
                {
                public:
                    /* [propget] */virtual HRESULT STDMETHODCALLTYPE get_Description(
                        /* [retval, out] */__RPC__out ABI_PARAMETER(Windows::Graphics::DirectX::Direct3D11::Direct3DSurfaceDescription) * value
                        ) = 0;
                    
                };

                MIDL_CONST_ID IID & IID_IDirect3DSurface=__uuidof(IDirect3DSurface);
                
            } /* Direct3D11 */
        } /* DirectX */
    } /* Graphics */
} /* Windows */
ABI_NAMESPACE_END

EXTERN_C const IID C_IID(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface);
#endif /* !defined(____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface_INTERFACE_DEFINED__) */
#endif // WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000


#else // !defined(__cplusplus)
/* Forward Declarations */
#ifndef ____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice_FWD_DEFINED__
#define ____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice_FWD_DEFINED__
typedef interface C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice) C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice);

#endif // ____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice_FWD_DEFINED__

#ifndef ____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface_FWD_DEFINED__
#define ____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface_FWD_DEFINED__
typedef interface C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface) C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface);

#endif // ____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface_FWD_DEFINED__


#pragma warning (push)
#pragma warning (disable:4668) 
#pragma warning (disable:4001) 
#pragma once 
#pragma warning (pop)



/*
 *
 * Typedef of Windows.Graphics.DirectX.Direct3D11.Direct3DMultisampleDescription
 *
 * Introduced to Windows.Foundation.UniversalApiContract in version 1.0
 *
 *
 */
#if WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000
/* [contract, version, version] */
typedef 
struct C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CDirect3DMultisampleDescription)
{
    INT32 Count;
    INT32 Quality;
} C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CDirect3DMultisampleDescription);
#endif // WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000


/*
 *
 * Typedef of Windows.Graphics.DirectX.Direct3D11.Direct3DSurfaceDescription
 *
 * Introduced to Windows.Foundation.UniversalApiContract in version 1.0
 *
 *
 */
#if WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000
/* [contract, version, version] */
typedef 
struct C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CDirect3DSurfaceDescription)
{
    INT32 Width;
    INT32 Height;
    C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirectXPixelFormat) Format;
    C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CDirect3DMultisampleDescription) MultisampleDescription;
} C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CDirect3DSurfaceDescription);
#endif // WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000


/*
 *
 * Typedef of Windows.Graphics.DirectX.Direct3D11.Direct3DUsage
 *
 * Introduced to Windows.Foundation.UniversalApiContract in version 1.0
 *
 *
 */
#if WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000
/* [contract, version, version] */
typedef /* [v1_enum] */
enum C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CDirect3DUsage)
{
    Direct3DUsage_Default = 0,
    Direct3DUsage_Immutable = 1,
    Direct3DUsage_Dynamic = 2,
    Direct3DUsage_Staging = 3,
} C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CDirect3DUsage);
#endif // WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000


/*
 *
 * Typedef of Windows.Graphics.DirectX.Direct3D11.Direct3DBindings
 *
 * Introduced to Windows.Foundation.UniversalApiContract in version 1.0
 *
 *
 */
#if WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000
/* [contract, flags, version, version] */
typedef /* [v1_enum] */
enum C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CDirect3DBindings)
{
    Direct3DBindings_VertexBuffer = 0x1,
    Direct3DBindings_IndexBuffer = 0x2,
    Direct3DBindings_ConstantBuffer = 0x4,
    Direct3DBindings_ShaderResource = 0x8,
    Direct3DBindings_StreamOutput = 0x10,
    Direct3DBindings_RenderTarget = 0x20,
    Direct3DBindings_DepthStencil = 0x40,
    Direct3DBindings_UnorderedAccess = 0x80,
    Direct3DBindings_Decoder = 0x200,
    Direct3DBindings_VideoEncoder = 0x400,
} C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CDirect3DBindings);
#endif // WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000


/*
 *
 * Interface Windows.Graphics.DirectX.Direct3D11.IDirect3DDevice
 *
 * Introduced to Windows.Foundation.UniversalApiContract in version 1.0
 *
 *
 * Any object which implements this interface must also implement the following interfaces:
 *     Windows.Foundation.IClosable
 *
 *
 */
#if WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000
#if !defined(____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice_INTERFACE_DEFINED__)
#define ____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice_INTERFACE_DEFINED__
extern const __declspec(selectany) _Null_terminated_ WCHAR InterfaceName_Windows_Graphics_DirectX_Direct3D11_IDirect3DDevice[] = L"Windows.Graphics.DirectX.Direct3D11.IDirect3DDevice";
/* [object, contract, uuid("A37624AB-8D5F-4650-9D3E-9EAE3D9BC670"), version, version] */
typedef struct C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDeviceVtbl)
{
    BEGIN_INTERFACE
    HRESULT ( STDMETHODCALLTYPE *QueryInterface)(
    __RPC__in C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice) * This,
    /* [in] */ __RPC__in REFIID riid,
    /* [annotation][iid_is][out] */
    _COM_Outptr_  void **ppvObject
    );

ULONG ( STDMETHODCALLTYPE *AddRef )(
    __RPC__in C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice) * This
    );

ULONG ( STDMETHODCALLTYPE *Release )(
    __RPC__in C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice) * This
    );

HRESULT ( STDMETHODCALLTYPE *GetIids )(
    __RPC__in C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice) * This,
    /* [out] */ __RPC__out ULONG *iidCount,
    /* [size_is][size_is][out] */ __RPC__deref_out_ecount_full_opt(*iidCount) IID **iids
    );

HRESULT ( STDMETHODCALLTYPE *GetRuntimeClassName )(
    __RPC__in C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice) * This,
    /* [out] */ __RPC__deref_out_opt HSTRING *className
    );

HRESULT ( STDMETHODCALLTYPE *GetTrustLevel )(
    __RPC__in C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice) * This,
    /* [OUT ] */ __RPC__out TrustLevel *trustLevel
    );
HRESULT ( STDMETHODCALLTYPE *Trim )(
        C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice) * This
        );
    END_INTERFACE
    
} C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDeviceVtbl);

interface C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice)
{
    CONST_VTBL struct C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDeviceVtbl) *lpVtbl;
};

#ifdef COBJMACROS
#define C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice)_QueryInterface(This,riid,ppvObject) \
( (This)->lpVtbl->QueryInterface(This,riid,ppvObject) )

#define C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice)_AddRef(This) \
        ( (This)->lpVtbl->AddRef(This) )

#define C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice)_Release(This) \
        ( (This)->lpVtbl->Release(This) )

#define C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice)_GetIids(This,iidCount,iids) \
        ( (This)->lpVtbl->GetIids(This,iidCount,iids) )

#define C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice)_GetRuntimeClassName(This,className) \
        ( (This)->lpVtbl->GetRuntimeClassName(This,className) )

#define C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice)_GetTrustLevel(This,trustLevel) \
        ( (This)->lpVtbl->GetTrustLevel(This,trustLevel) )

#define __x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice_Trim(This) \
    ( (This)->lpVtbl->Trim(This) )


#endif /* COBJMACROS */


EXTERN_C const IID C_IID(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice);
#endif /* !defined(____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DDevice_INTERFACE_DEFINED__) */
#endif // WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000


/*
 *
 * Interface Windows.Graphics.DirectX.Direct3D11.IDirect3DSurface
 *
 * Introduced to Windows.Foundation.UniversalApiContract in version 1.0
 *
 *
 * Any object which implements this interface must also implement the following interfaces:
 *     Windows.Foundation.IClosable
 *
 *
 */
#if WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000
#if !defined(____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface_INTERFACE_DEFINED__)
#define ____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface_INTERFACE_DEFINED__
extern const __declspec(selectany) _Null_terminated_ WCHAR InterfaceName_Windows_Graphics_DirectX_Direct3D11_IDirect3DSurface[] = L"Windows.Graphics.DirectX.Direct3D11.IDirect3DSurface";
/* [object, contract, uuid("0BF4A146-13C1-4694-BEE3-7ABF15EAF586"), version, version] */
typedef struct C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurfaceVtbl)
{
    BEGIN_INTERFACE
    HRESULT ( STDMETHODCALLTYPE *QueryInterface)(
    __RPC__in C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface) * This,
    /* [in] */ __RPC__in REFIID riid,
    /* [annotation][iid_is][out] */
    _COM_Outptr_  void **ppvObject
    );

ULONG ( STDMETHODCALLTYPE *AddRef )(
    __RPC__in C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface) * This
    );

ULONG ( STDMETHODCALLTYPE *Release )(
    __RPC__in C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface) * This
    );

HRESULT ( STDMETHODCALLTYPE *GetIids )(
    __RPC__in C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface) * This,
    /* [out] */ __RPC__out ULONG *iidCount,
    /* [size_is][size_is][out] */ __RPC__deref_out_ecount_full_opt(*iidCount) IID **iids
    );

HRESULT ( STDMETHODCALLTYPE *GetRuntimeClassName )(
    __RPC__in C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface) * This,
    /* [out] */ __RPC__deref_out_opt HSTRING *className
    );

HRESULT ( STDMETHODCALLTYPE *GetTrustLevel )(
    __RPC__in C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface) * This,
    /* [OUT ] */ __RPC__out TrustLevel *trustLevel
    );
/* [propget] */HRESULT ( STDMETHODCALLTYPE *get_Description )(
        C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface) * This,
        /* [retval, out] */__RPC__out C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CDirect3DSurfaceDescription) * value
        );
    END_INTERFACE
    
} C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurfaceVtbl);

interface C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface)
{
    CONST_VTBL struct C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurfaceVtbl) *lpVtbl;
};

#ifdef COBJMACROS
#define C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface)_QueryInterface(This,riid,ppvObject) \
( (This)->lpVtbl->QueryInterface(This,riid,ppvObject) )

#define C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface)_AddRef(This) \
        ( (This)->lpVtbl->AddRef(This) )

#define C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface)_Release(This) \
        ( (This)->lpVtbl->Release(This) )

#define C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface)_GetIids(This,iidCount,iids) \
        ( (This)->lpVtbl->GetIids(This,iidCount,iids) )

#define C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface)_GetRuntimeClassName(This,className) \
        ( (This)->lpVtbl->GetRuntimeClassName(This,className) )

#define C_ABI_PARAMETER(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface)_GetTrustLevel(This,trustLevel) \
        ( (This)->lpVtbl->GetTrustLevel(This,trustLevel) )

#define __x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface_get_Description(This,value) \
    ( (This)->lpVtbl->get_Description(This,value) )


#endif /* COBJMACROS */


EXTERN_C const IID C_IID(Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface);
#endif /* !defined(____x_Windows_CGraphics_CDirectX_CDirect3D11_CIDirect3DSurface_INTERFACE_DEFINED__) */
#endif // WINDOWS_FOUNDATION_UNIVERSALAPICONTRACT_VERSION >= 0x10000


#endif // defined(__cplusplus)
#pragma pop_macro("MIDL_CONST_ID")
#pragma pop_macro("C_IID")
#pragma pop_macro("ABI_CONCAT")
#pragma pop_macro("ABI_PARAMETER")
#pragma pop_macro("ABI_NAMESPACE_BEGIN")
#pragma pop_macro("ABI_NAMESPACE_END")


#endif // __winrtdirect3d11_p_h__

#endif // __winrtdirect3d11_h__
