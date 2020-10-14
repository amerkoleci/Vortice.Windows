

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

#ifndef __microsoft2Eui2Examl2Examlroot_h__
#define __microsoft2Eui2Examl2Examlroot_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IXamlRootNative_FWD_DEFINED__
#define __IXamlRootNative_FWD_DEFINED__
typedef interface IXamlRootNative IXamlRootNative;

#endif 	/* __IXamlRootNative_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IXamlRootNative_INTERFACE_DEFINED__
#define __IXamlRootNative_INTERFACE_DEFINED__

/* interface IXamlRootNative */
/* [unique][local][uuid][object] */ 


EXTERN_C const IID IID_IXamlRootNative;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("4CCD7521-9C08-41AD-A5BD-B263EF64C9E7")
    IXamlRootNative : public IUnknown
    {
    public:
        virtual /* [propget] */ HRESULT STDMETHODCALLTYPE get_HostWindow( 
            /* [retval][out] */ HWND *hWnd) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IXamlRootNativeVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IXamlRootNative * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IXamlRootNative * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IXamlRootNative * This);
        
        /* [propget] */ HRESULT ( STDMETHODCALLTYPE *get_HostWindow )( 
            IXamlRootNative * This,
            /* [retval][out] */ HWND *hWnd);
        
        END_INTERFACE
    } IXamlRootNativeVtbl;

    interface IXamlRootNative
    {
        CONST_VTBL struct IXamlRootNativeVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IXamlRootNative_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IXamlRootNative_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IXamlRootNative_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IXamlRootNative_get_HostWindow(This,hWnd)	\
    ( (This)->lpVtbl -> get_HostWindow(This,hWnd) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IXamlRootNative_INTERFACE_DEFINED__ */


/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


