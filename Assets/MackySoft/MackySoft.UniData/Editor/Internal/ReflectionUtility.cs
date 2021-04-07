using System;
using System.Linq;
using System.Collections.Generic;

namespace MackySoft.UniData.Internal {

	public static class ReflectionUtility {

		public static IEnumerable<Type> GetAllTypes () {
			return AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(assembly => assembly.GetTypes());
		}

		public static IEnumerable<Type> GetAllTypes (Func<Type,bool> predicate) {
			return AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(assembly => assembly.GetTypes())
				.Where(predicate);
		}


		public static bool IsAssignableFromRecursive (this Type type,Type c) {
			Type t = c;
			while (t != null) {
				if (type.IsAssignableFrom(t)) {
					return true;
				}
				t = t.BaseType;
			}
			return false;
		}

		public static bool IsSubclassOfRecursive (this Type type,Type c) {
			Type t = type;
			while (t != null) {
				if (t.IsSubclassOf(c)) {
					return true;
				}
				t = t.BaseType;
			}
			return false;
		}

		public static Type FindTypeRecursive (this Type type,Type c) {
			if (c.IsClass) {

			}
			else if (c.IsInterface) {
				while (true) {
					var interfaces = type.GetInterfaces();
					if (interfaces.Length == 0) {
						break;
					}
					for (int i = 0;i < interfaces.Length;i++) {
						var interfaceType = interfaces[i];
						if (interfaceType == c) {
							return interfaceType;
						}
					}
					type = type.BaseType;
				}
			}
			return null;
		}

		public static Type FindGenericTypeFromDefinition (this Type type,Type genericDefinition) {
			if (!genericDefinition.IsGenericTypeDefinition) {
				return null;
			}
			if (genericDefinition.IsClass) {
				while (type != null) {
					if (type.IsGenericType && type.GetGenericTypeDefinition() == genericDefinition) {
						return type;
					}

					type = type.BaseType;
				}
			}
			else if (genericDefinition.IsInterface) {
				while (type != null) {
					foreach (var interfaceType in type.GetInterfaces()) {
						if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == genericDefinition) {
							return interfaceType;
						}
					}
					type = type.BaseType;
				}
			}
			
			return null;
		}

		public static bool IsCollectionType (this Type type) {
			if (type.IsArray) {
				return true;
			}
			if (!type.IsGenericType) {
				return false;
			}
			Type genericTypeDefinition = type.GetGenericTypeDefinition();
			if (genericTypeDefinition == typeof(List<>)) {
				return true;
			}
			return false;
		}

		public static Type GetCollectionElementType (this Type type) {
			if (type.IsArray) {
				return type.GetElementType();
			}

			Type genericTypeDefinition = type.GetGenericTypeDefinition();
			if (genericTypeDefinition == typeof(List<>)) {
				return type.GetGenericArguments()[0];
			}

			return null;
		}

	}
}