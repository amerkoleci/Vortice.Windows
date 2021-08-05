

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.01.0622 */
/* @@MIDL_FILE_HEADING(  ) */



/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 500
#endif

/* verify that the <rpcsal.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCSAL_H_VERSION__
#define __REQUIRED_RPCSAL_H_VERSION__ 100
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif /* __RPCNDR_H_VERSION__ */

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __microsoft2Eui2Examl2Ehosting2Edesktopwindowxamlsource_h__
#define __microsoft2Eui2Examl2Ehosting2Edesktopwindowxamlsource_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IDesktopWindowXamlSourceNative_FWD_DEFINED__
#define __IDesktopWindowXamlSourceNative_FWD_DEFINED__
typedef interface IDesktopWindowXamlSourceNative IDesktopWindowXamlSourceNative;

#endif 	/* __IDesktopWindowXamlSourceNative_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IDesktopWindowXamlSourceNative_INTERFACE_DEFINED__
#define __IDesktopWindowXamlSourceNative_INTERFACE_DEFINED__

/* interface IDesktopWindowXamlSourceNative */
/* [unique][local][uuid][object] */ 


EXTERN_C const IID IID_IDesktopWindowXamlSourceNative;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("0aea2f26-facf-4588-8cf4-34555124db32")
    IDesktopWindowXamlSourceNative : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE AttachToWindow( 
            /* [annotation][in] */ 
            _In_  HWND parentWnd) = 0;
        
        virtual /* [propget] */ HRESULT STDMETHODCALLTYPE get_WindowHandle( 
            /* [retval][out] */ HWND *hWnd) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE PreTranslateMessage( 
            /* [annotation][in] */ 
            _In_  const MSG *message,
            /* [retval][out] */ BOOL *result) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IDesktopWindowXamlSourceNativeVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IDesktopWindowXamlSourceNative * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IDesktopWindowXamlSourceNative * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IDesktopWindowXamlSourceNative * This);
        
        HRESULT ( STDMETHODCALLTYPE *AttachToWindow )( 
            IDesktopWindowXamlSourceNative * This,
            /* [annotation][in] */ 
            _In_  HWND parentWnd);
        
        /* [propget] */ HRESULT ( STDMETHODCALLTYPE *get_WindowHandle )( 
            IDesktopWindowXamlSourceNative * This,
            /* [retval][out] */ HWND *hWnd);
        
        HRESULT ( STDMETHODCALLTYPE *PreTranslateMessage )( 
            IDesktopWindowXamlSourceNative * This,
            /* [annotation][in] */ 
            _In_  const MSG *message,
            /* [retval][out] */ BOOL *result);
        
        END_INTERFACE
    } IDesktopWindowXamlSourceNativeVtbl;

    interface IDesktopWindowXamlSourceNative
    {
        CONST_VTBL struct IDesktopWindowXamlSourceNativeVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IDesktopWindowXamlSourceNative_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IDesktopWindowXamlSourceNative_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IDesktopWindowXamlSourceNative_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IDesktopWindowXamlSourceNative_AttachToWindow(This,parentWnd)	\
    ( (This)->lpVtbl -> AttachToWindow(This,parentWnd) ) 

#define IDesktopWindowXamlSourceNative_get_WindowHandle(This,hWnd)	\
    ( (This)->lpVtbl -> get_WindowHandle(This,hWnd) ) 

#define IDesktopWindowXamlSourceNative_PreTranslateMessage(This,message,result)	\
    ( (This)->lpVtbl -> PreTranslateMessage(This,message,result) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IDesktopWindowXamlSourceNative_INTERFACE_DEFINED__ */


/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


