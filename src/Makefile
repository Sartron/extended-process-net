BUILD_CONFIG := Release
FILE_PROJ := $(wildcard */*.csproj)
PATH_BUILD := build

all: clean restore build test

clean:
	dotnet clean
	rm -fr ${PATH_BUILD}

restore:
	dotnet restore

.PHONY: build
build: ${FILE_PROJ}
	for ITER_FILE_PROJ in ${^}; do \
	  dotnet build "$${ITER_FILE_PROJ}" \
	  	--configuration "${BUILD_CONFIG}" \
	  	--output "${PATH_BUILD}/$${ITER_FILE_PROJ}"; \
	  mv "${PATH_BUILD}/$${ITER_FILE_PROJ}" "${PATH_BUILD}/"; \
	done
	rmdir --ignore-fail-on-non-empty "${PATH_BUILD}/"*

test:
	dotnet test

pack:
	dotnet pack \
	  --configuration "${BUILD_CONFIG}" \
	  --output "${PATH_BUILD}/$${ITER_FILE_PROJ}"

run:
	dotnet run
