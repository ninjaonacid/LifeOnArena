/*! For license information please see 690.1e1f8b8fc2d46c9e3dbf.js.LICENSE.txt */
(self.webpackChunkgame_score_sdk=self.webpackChunkgame_score_sdk||[]).push([[690],{8694:(n,t,e)=>{var _;function o(n){return(o="function"==typeof Symbol&&"symbol"==typeof Symbol.iterator?function(n){return typeof n}:function(n){return n&&"function"==typeof Symbol&&n.constructor===Symbol&&n!==Symbol.prototype?"symbol":typeof n})(n)}!function(){"use strict";var r={}.hasOwnProperty;function u(){for(var n=[],t=0;t<arguments.length;t++){var e=arguments[t];if(e){var _=o(e);if("string"===_||"number"===_)n.push(e);else if(Array.isArray(e)){if(e.length){var i=u.apply(null,e);i&&n.push(i)}}else if("object"===_)if(e.toString===Object.prototype.toString)for(var l in e)r.call(e,l)&&e[l]&&n.push(l);else n.push(e.toString())}}return n.join(" ")}n.exports?(u.default=u,n.exports=u):"object"===o(e.amdO)&&e.amdO?void 0===(_=function(){return u}.apply(t,[]))||(n.exports=_):window.classNames=u}()},2972:(n,t,e)=>{"use strict";e.r(t),e.d(t,{render:()=>L,hydrate:()=>O,createElement:()=>h,h:()=>h,Fragment:()=>y,createRef:()=>v,isValidElement:()=>o,Component:()=>m,cloneElement:()=>R,createContext:()=>V,toChildArray:()=>H,options:()=>_});var _,o,r,u,i,l,c={},f=[],s=/acit|ex(?:s|g|n|p|$)|rph|grid|ows|mnc|ntw|ine[ch]|zoo|^ord|itera/i;function a(n,t){for(var e in t)n[e]=t[e];return n}function p(n){var t=n.parentNode;t&&t.removeChild(n)}function h(n,t,e){var _,o,r,u=arguments,i={};for(r in t)"key"==r?_=t[r]:"ref"==r?o=t[r]:i[r]=t[r];if(arguments.length>3)for(e=[e],r=3;r<arguments.length;r++)e.push(u[r]);if(null!=e&&(i.children=e),"function"==typeof n&&null!=n.defaultProps)for(r in n.defaultProps)void 0===i[r]&&(i[r]=n.defaultProps[r]);return d(n,i,_,o,null)}function d(n,t,e,o,r){var u={type:n,props:t,key:e,ref:o,__k:null,__:null,__b:0,__e:null,__d:void 0,__c:null,__h:null,constructor:void 0,__v:null==r?++_.__v:r};return null!=_.vnode&&_.vnode(u),u}function v(){return{current:null}}function y(n){return n.children}function m(n,t){this.props=n,this.context=t}function g(n,t){if(null==t)return n.__?g(n.__,n.__.__k.indexOf(n)+1):null;for(var e;t<n.__k.length;t++)if(null!=(e=n.__k[t])&&null!=e.__e)return e.__e;return"function"==typeof n.type?g(n):null}function b(n){var t,e;if(null!=(n=n.__)&&null!=n.__c){for(n.__e=n.__c.base=null,t=0;t<n.__k.length;t++)if(null!=(e=n.__k[t])&&null!=e.__e){n.__e=n.__c.base=e.__e;break}return b(n)}}function k(n){(!n.__d&&(n.__d=!0)&&r.push(n)&&!C.__r++||i!==_.debounceRendering)&&((i=_.debounceRendering)||u)(C)}function C(){for(var n;C.__r=r.length;)n=r.sort((function(n,t){return n.__v.__b-t.__v.__b})),r=[],n.some((function(n){var t,e,_,o,r,u;n.__d&&(r=(o=(t=n).__v).__e,(u=t.__P)&&(e=[],(_=a({},o)).__v=o.__v+1,T(u,o,_,t.__n,void 0!==u.ownerSVGElement,null!=o.__h?[r]:null,e,null==r?g(o):r,o.__h),U(e,o),o.__e!=r&&b(o)))}))}function S(n,t,e,_,o,r,u,i,l,s){var a,p,h,v,m,b,k,C=_&&_.__k||f,S=C.length;for(e.__k=[],a=0;a<t.length;a++)if(null!=(v=e.__k[a]=null==(v=t[a])||"boolean"==typeof v?null:"string"==typeof v||"number"==typeof v||"bigint"==typeof v?d(null,v,null,null,v):Array.isArray(v)?d(y,{children:v},null,null,null):v.__b>0?d(v.type,v.props,v.key,null,v.__v):v)){if(v.__=e,v.__b=e.__b+1,null===(h=C[a])||h&&v.key==h.key&&v.type===h.type)C[a]=void 0;else for(p=0;p<S;p++){if((h=C[p])&&v.key==h.key&&v.type===h.type){C[p]=void 0;break}h=null}T(n,v,h=h||c,o,r,u,i,l,s),m=v.__e,(p=v.ref)&&h.ref!=p&&(k||(k=[]),h.ref&&k.push(h.ref,null,v),k.push(p,v.__c||m,v)),null!=m?(null==b&&(b=m),"function"==typeof v.type&&null!=v.__k&&v.__k===h.__k?v.__d=l=x(v,l,n):l=E(n,v,h,C,m,l),s||"option"!==e.type?"function"==typeof e.type&&(e.__d=l):n.value=""):l&&h.__e==l&&l.parentNode!=n&&(l=g(h))}for(e.__e=b,a=S;a--;)null!=C[a]&&("function"==typeof e.type&&null!=C[a].__e&&C[a].__e==e.__d&&(e.__d=g(_,a+1)),M(C[a],C[a]));if(k)for(a=0;a<k.length;a++)W(k[a],k[++a],k[++a])}function x(n,t,e){var _,o;for(_=0;_<n.__k.length;_++)(o=n.__k[_])&&(o.__=n,t="function"==typeof o.type?x(o,t,e):E(e,o,o,n.__k,o.__e,t));return t}function H(n,t){return t=t||[],null==n||"boolean"==typeof n||(Array.isArray(n)?n.some((function(n){H(n,t)})):t.push(n)),t}function E(n,t,e,_,o,r){var u,i,l;if(void 0!==t.__d)u=t.__d,t.__d=void 0;else if(null==e||o!=r||null==o.parentNode)n:if(null==r||r.parentNode!==n)n.appendChild(o),u=null;else{for(i=r,l=0;(i=i.nextSibling)&&l<_.length;l+=2)if(i==o)break n;n.insertBefore(o,r),u=r}return void 0!==u?u:o.nextSibling}function P(n,t,e){"-"===t[0]?n.setProperty(t,e):n[t]=null==e?"":"number"!=typeof e||s.test(t)?e:e+"px"}function w(n,t,e,_,o){var r;n:if("style"===t)if("string"==typeof e)n.style.cssText=e;else{if("string"==typeof _&&(n.style.cssText=_=""),_)for(t in _)e&&t in e||P(n.style,t,"");if(e)for(t in e)_&&e[t]===_[t]||P(n.style,t,e[t])}else if("o"===t[0]&&"n"===t[1])r=t!==(t=t.replace(/Capture$/,"")),t=t.toLowerCase()in n?t.toLowerCase().slice(2):t.slice(2),n.l||(n.l={}),n.l[t+r]=e,e?_||n.addEventListener(t,r?D:A,r):n.removeEventListener(t,r?D:A,r);else if("dangerouslySetInnerHTML"!==t){if(o)t=t.replace(/xlink[H:h]/,"h").replace(/sName$/,"s");else if("href"!==t&&"list"!==t&&"form"!==t&&"tabIndex"!==t&&"download"!==t&&t in n)try{n[t]=null==e?"":e;break n}catch(n){}"function"==typeof e||(null!=e&&(!1!==e||"a"===t[0]&&"r"===t[1])?n.setAttribute(t,e):n.removeAttribute(t))}}function A(n){this.l[n.type+!1](_.event?_.event(n):n)}function D(n){this.l[n.type+!0](_.event?_.event(n):n)}function T(n,t,e,o,r,u,i,l,c){var f,s,p,h,d,v,g,b,k,C,x,H=t.type;if(void 0!==t.constructor)return null;null!=e.__h&&(c=e.__h,l=t.__e=e.__e,t.__h=null,u=[l]),(f=_.__b)&&f(t);try{n:if("function"==typeof H){if(b=t.props,k=(f=H.contextType)&&o[f.__c],C=f?k?k.props.value:f.__:o,e.__c?g=(s=t.__c=e.__c).__=s.__E:("prototype"in H&&H.prototype.render?t.__c=s=new H(b,C):(t.__c=s=new m(b,C),s.constructor=H,s.render=N),k&&k.sub(s),s.props=b,s.state||(s.state={}),s.context=C,s.__n=o,p=s.__d=!0,s.__h=[]),null==s.__s&&(s.__s=s.state),null!=H.getDerivedStateFromProps&&(s.__s==s.state&&(s.__s=a({},s.__s)),a(s.__s,H.getDerivedStateFromProps(b,s.__s))),h=s.props,d=s.state,p)null==H.getDerivedStateFromProps&&null!=s.componentWillMount&&s.componentWillMount(),null!=s.componentDidMount&&s.__h.push(s.componentDidMount);else{if(null==H.getDerivedStateFromProps&&b!==h&&null!=s.componentWillReceiveProps&&s.componentWillReceiveProps(b,C),!s.__e&&null!=s.shouldComponentUpdate&&!1===s.shouldComponentUpdate(b,s.__s,C)||t.__v===e.__v){s.props=b,s.state=s.__s,t.__v!==e.__v&&(s.__d=!1),s.__v=t,t.__e=e.__e,t.__k=e.__k,t.__k.forEach((function(n){n&&(n.__=t)})),s.__h.length&&i.push(s);break n}null!=s.componentWillUpdate&&s.componentWillUpdate(b,s.__s,C),null!=s.componentDidUpdate&&s.__h.push((function(){s.componentDidUpdate(h,d,v)}))}s.context=C,s.props=b,s.state=s.__s,(f=_.__r)&&f(t),s.__d=!1,s.__v=t,s.__P=n,f=s.render(s.props,s.state,s.context),s.state=s.__s,null!=s.getChildContext&&(o=a(a({},o),s.getChildContext())),p||null==s.getSnapshotBeforeUpdate||(v=s.getSnapshotBeforeUpdate(h,d)),x=null!=f&&f.type===y&&null==f.key?f.props.children:f,S(n,Array.isArray(x)?x:[x],t,e,o,r,u,i,l,c),s.base=t.__e,t.__h=null,s.__h.length&&i.push(s),g&&(s.__E=s.__=null),s.__e=!1}else null==u&&t.__v===e.__v?(t.__k=e.__k,t.__e=e.__e):t.__e=F(e.__e,t,e,o,r,u,i,c);(f=_.diffed)&&f(t)}catch(n){t.__v=null,(c||null!=u)&&(t.__e=l,t.__h=!!c,u[u.indexOf(l)]=null),_.__e(n,t,e)}}function U(n,t){_.__c&&_.__c(t,n),n.some((function(t){try{n=t.__h,t.__h=[],n.some((function(n){n.call(t)}))}catch(n){_.__e(n,t.__v)}}))}function F(n,t,e,_,o,r,u,i){var l,s,a,h,d=e.props,v=t.props,y=t.type,m=0;if("svg"===y&&(o=!0),null!=r)for(;m<r.length;m++)if((l=r[m])&&(l===n||(y?l.localName==y:3==l.nodeType))){n=l,r[m]=null;break}if(null==n){if(null===y)return document.createTextNode(v);n=o?document.createElementNS("http://www.w3.org/2000/svg",y):document.createElement(y,v.is&&v),r=null,i=!1}if(null===y)d===v||i&&n.data===v||(n.data=v);else{if(r=r&&f.slice.call(n.childNodes),s=(d=e.props||c).dangerouslySetInnerHTML,a=v.dangerouslySetInnerHTML,!i){if(null!=r)for(d={},h=0;h<n.attributes.length;h++)d[n.attributes[h].name]=n.attributes[h].value;(a||s)&&(a&&(s&&a.__html==s.__html||a.__html===n.innerHTML)||(n.innerHTML=a&&a.__html||""))}if(function(n,t,e,_,o){var r;for(r in e)"children"===r||"key"===r||r in t||w(n,r,null,e[r],_);for(r in t)o&&"function"!=typeof t[r]||"children"===r||"key"===r||"value"===r||"checked"===r||e[r]===t[r]||w(n,r,t[r],e[r],_)}(n,v,d,o,i),a)t.__k=[];else if(m=t.props.children,S(n,Array.isArray(m)?m:[m],t,e,_,o&&"foreignObject"!==y,r,u,n.firstChild,i),null!=r)for(m=r.length;m--;)null!=r[m]&&p(r[m]);i||("value"in v&&void 0!==(m=v.value)&&(m!==n.value||"progress"===y&&!m)&&w(n,"value",m,d.value,!1),"checked"in v&&void 0!==(m=v.checked)&&m!==n.checked&&w(n,"checked",m,d.checked,!1))}return n}function W(n,t,e){try{"function"==typeof n?n(t):n.current=t}catch(n){_.__e(n,e)}}function M(n,t,e){var o,r,u;if(_.unmount&&_.unmount(n),(o=n.ref)&&(o.current&&o.current!==n.__e||W(o,null,t)),e||"function"==typeof n.type||(e=null!=(r=n.__e)),n.__e=n.__d=void 0,null!=(o=n.__c)){if(o.componentWillUnmount)try{o.componentWillUnmount()}catch(n){_.__e(n,t)}o.base=o.__P=null}if(o=n.__k)for(u=0;u<o.length;u++)o[u]&&M(o[u],t,e);null!=r&&p(r)}function N(n,t,e){return this.constructor(n,e)}function L(n,t,e){var o,r,u;_.__&&_.__(n,t),r=(o="function"==typeof e)?null:e&&e.__k||t.__k,u=[],T(t,n=(!o&&e||t).__k=h(y,null,[n]),r||c,c,void 0!==t.ownerSVGElement,!o&&e?[e]:r?null:t.firstChild?f.slice.call(t.childNodes):null,u,!o&&e?e:r?r.__e:t.firstChild,o),U(u,n)}function O(n,t){L(n,t,O)}function R(n,t,e){var _,o,r,u=arguments,i=a({},n.props);for(r in t)"key"==r?_=t[r]:"ref"==r?o=t[r]:i[r]=t[r];if(arguments.length>3)for(e=[e],r=3;r<arguments.length;r++)e.push(u[r]);return null!=e&&(i.children=e),d(n.type,i,_||n.key,o||n.ref,null)}function V(n,t){var e={__c:t="__cC"+l++,__:n,Consumer:function(n,t){return n.children(t)},Provider:function(n){var e,_;return this.getChildContext||(e=[],(_={})[t]=this,this.getChildContext=function(){return _},this.shouldComponentUpdate=function(n){this.props.value!==n.value&&e.some(k)},this.sub=function(n){e.push(n);var t=n.componentWillUnmount;n.componentWillUnmount=function(){e.splice(e.indexOf(n),1),t&&t.call(n)}}),n.children}};return e.Provider.__=e.Consumer.contextType=e}_={__e:function(n,t){for(var e,_,o;t=t.__;)if((e=t.__c)&&!e.__)try{if((_=e.constructor)&&null!=_.getDerivedStateFromError&&(e.setState(_.getDerivedStateFromError(n)),o=e.__d),null!=e.componentDidCatch&&(e.componentDidCatch(n),o=e.__d),o)return e.__E=e}catch(t){n=t}throw n},__v:0},o=function(n){return null!=n&&void 0===n.constructor},m.prototype.setState=function(n,t){var e;e=null!=this.__s&&this.__s!==this.state?this.__s:this.__s=a({},this.state),"function"==typeof n&&(n=n(a({},e),this.props)),n&&a(e,n),null!=n&&this.__v&&(t&&this.__h.push(t),k(this))},m.prototype.forceUpdate=function(n){this.__v&&(this.__e=!0,n&&this.__h.push(n),k(this))},m.prototype.render=y,r=[],u="function"==typeof Promise?Promise.prototype.then.bind(Promise.resolve()):setTimeout,C.__r=0,l=0},6059:(n,t,e)=>{"use strict";e.r(t),e.d(t,{useState:()=>d,useReducer:()=>v,useEffect:()=>y,useLayoutEffect:()=>m,useRef:()=>g,useImperativeHandle:()=>b,useMemo:()=>k,useCallback:()=>C,useContext:()=>S,useDebugValue:()=>x,useErrorBoundary:()=>H});var _,o,r,u=e(2972),i=0,l=[],c=u.options.__b,f=u.options.__r,s=u.options.diffed,a=u.options.__c,p=u.options.unmount;function h(n,t){u.options.__h&&u.options.__h(o,n,i||t),i=0;var e=o.__H||(o.__H={__:[],__h:[]});return n>=e.__.length&&e.__.push({}),e.__[n]}function d(n){return i=1,v(T,n)}function v(n,t,e){var r=h(_++,2);return r.t=n,r.__c||(r.__=[e?e(t):T(void 0,t),function(n){var t=r.t(r.__[0],n);r.__[0]!==t&&(r.__=[t,r.__[1]],r.__c.setState({}))}],r.__c=o),r.__}function y(n,t){var e=h(_++,3);!u.options.__s&&D(e.__H,t)&&(e.__=n,e.__H=t,o.__H.__h.push(e))}function m(n,t){var e=h(_++,4);!u.options.__s&&D(e.__H,t)&&(e.__=n,e.__H=t,o.__h.push(e))}function g(n){return i=5,k((function(){return{current:n}}),[])}function b(n,t,e){i=6,m((function(){"function"==typeof n?n(t()):n&&(n.current=t())}),null==e?e:e.concat(n))}function k(n,t){var e=h(_++,7);return D(e.__H,t)&&(e.__=n(),e.__H=t,e.__h=n),e.__}function C(n,t){return i=8,k((function(){return n}),t)}function S(n){var t=o.context[n.__c],e=h(_++,9);return e.__c=n,t?(null==e.__&&(e.__=!0,t.sub(o)),t.props.value):n.__}function x(n,t){u.options.useDebugValue&&u.options.useDebugValue(t?t(n):n)}function H(n){var t=h(_++,10),e=d();return t.__=n,o.componentDidCatch||(o.componentDidCatch=function(n){t.__&&t.__(n),e[1](n)}),[e[0],function(){e[1](void 0)}]}function E(){l.forEach((function(n){if(n.__P)try{n.__H.__h.forEach(w),n.__H.__h.forEach(A),n.__H.__h=[]}catch(t){n.__H.__h=[],u.options.__e(t,n.__v)}})),l=[]}u.options.__b=function(n){o=null,c&&c(n)},u.options.__r=function(n){f&&f(n),_=0;var t=(o=n.__c).__H;t&&(t.__h.forEach(w),t.__h.forEach(A),t.__h=[])},u.options.diffed=function(n){s&&s(n);var t=n.__c;t&&t.__H&&t.__H.__h.length&&(1!==l.push(t)&&r===u.options.requestAnimationFrame||((r=u.options.requestAnimationFrame)||function(n){var t,e=function(){clearTimeout(_),P&&cancelAnimationFrame(t),setTimeout(n)},_=setTimeout(e,100);P&&(t=requestAnimationFrame(e))})(E)),o=void 0},u.options.__c=function(n,t){t.some((function(n){try{n.__h.forEach(w),n.__h=n.__h.filter((function(n){return!n.__||A(n)}))}catch(e){t.some((function(n){n.__h&&(n.__h=[])})),t=[],u.options.__e(e,n.__v)}})),a&&a(n,t)},u.options.unmount=function(n){p&&p(n);var t=n.__c;if(t&&t.__H)try{t.__H.__.forEach(w)}catch(n){u.options.__e(n,t.__v)}};var P="function"==typeof requestAnimationFrame;function w(n){var t=o;"function"==typeof n.__c&&n.__c(),o=t}function A(n){var t=o;n.__c=n.__(),o=t}function D(n,t){return!n||n.length!==t.length||t.some((function(t,e){return t!==n[e]}))}function T(n,t){return"function"==typeof t?t(n):t}},2095:(n,t,e)=>{"use strict";e.d(t,{HY:()=>_.Fragment,tZ:()=>o,BX:()=>o});var _=e(2972);function o(n,t,e,o,r){var u={};for(var i in t)"ref"!=i&&(u[i]=t[i]);var l,c,f={type:n,props:u,key:e,ref:t&&t.ref,__k:null,__:null,__b:0,__e:null,__d:void 0,__c:null,__h:null,constructor:void 0,__v:++_.options.__v,__source:o,__self:r};if("function"==typeof n&&(l=n.defaultProps))for(c in l)void 0===u[c]&&(u[c]=l[c]);return _.options.vnode&&_.options.vnode(f),f}},4483:(n,t,e)=>{"use strict";function _(n,t){for(var e in t)n[e]=t[e];return n}function o(n){var t=[];function e(n){for(var e=[],_=0;_<t.length;_++)t[_]===n?n=null:e.push(t[_]);t=e}function o(e,o,r){n=o?e:_(_({},n),e);for(var u=t,i=0;i<u.length;i++)u[i](n,r)}return n=n||{},{action:function(t){function e(n){o(n,!1,t)}return function(){for(var _=arguments,o=[n],r=0;r<arguments.length;r++)o.push(_[r]);var u=t.apply(this,o);if(null!=u)return u.then?u.then(e):e(u)}},setState:o,subscribe:function(n){return t.push(n),function(){e(n)}},unsubscribe:e,getState:function(){return n}}}e.d(t,{Z:()=>o})},3341:(n,t,e)=>{var _=e(2972);function o(n,t){for(var e in t)n[e]=t[e];return n}function r(n){this.getChildContext=function(){return{store:n.store}}}r.prototype.render=function(n){return n.children&&n.children[0]||n.children},t.$=function(n,t){var e;return"function"!=typeof n&&("string"==typeof(e=n||{})&&(e=e.split(/\s*,\s*/)),n=function(n){for(var t={},_=0;_<e.length;_++)t[e[_]]=n[e[_]];return t}),function(e){function r(r,u){var i=this,l=u.store,c=n(l?l.getState():{},r),f=t?function(n,t){"function"==typeof n&&(n=n(t));var e={};for(var _ in n)e[_]=t.action(n[_]);return e}(t,l):{store:l},s=function(){var t=n(l?l.getState():{},r);for(var e in t)if(t[e]!==c[e])return c=t,i.setState({});for(var _ in c)if(!(_ in t))return c=t,i.setState({})};this.componentWillReceiveProps=function(n){r=n,s()},this.componentDidMount=function(){l.subscribe(s)},this.componentWillUnmount=function(){l.unsubscribe(s)},this.render=function(n){return _.h(e,o(o(o({},f),n),c))}}return(r.prototype=new _.Component).constructor=r}},t.z=r}}]);