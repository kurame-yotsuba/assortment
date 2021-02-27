from os import makedirs
from os.path import basename, exists, isdir, isfile, join
from shutil import copy, copytree, ignore_patterns, rmtree
from typing import Iterable, List, TypedDict

'''
base_dir/include を dist_base_dir/dist にコピー
exclude_patternsは除く
'''


class BuildTargetRequired(TypedDict):
    include: List[str]


class BuildTarget(BuildTargetRequired, total=False):
    include_base: str
    dist: str
    exclude_patterns: List[str]


class BuildSetting(TypedDict):
    dist_base: str
    targets: Iterable[BuildTarget]


docs_dir = 'docs'


settings: BuildSetting = {
    'dist_base': 'dist',
    'targets': [
        {
            'include': ['src'],
            'exclude_patterns': ['__pycache__'],
        },
        {
            'include_base': docs_dir,
            'include': [
                'sample.txt',
            ],
            'dist': docs_dir
        }
    ]
}


def get_includes(target: BuildTarget) -> List[str]:
    '''
    include_baseとincludeをjoinする。
    '''
    include_base_dir = target.get('include_base', '')
    return [
        join(include_base_dir, x) for x in target['include']
    ]


def check() -> bool:
    not_found_list = []
    for target in settings['targets']:
        include_list = get_includes(target)
        not_found_list += [x for x in include_list if not exists(x)]

    if len(not_found_list) > 0:
        for not_found in not_found_list:
            print(f'{not_found} が見つかりません。')
        return False
    else:
        return True


def clean():
    '''
    出力ディレクトリの再作成を行う。
    '''

    dist_dir = settings['dist_base']
    if exists(dist_dir):
        rmtree(dist_dir)

    makedirs(dist_dir, exist_ok=True)


def main():
    '''
    ソースファイル、その他を配布用ディレクトリに出力する。
    '''

    if check() is False:
        print('ビルド失敗')
        return

    clean()

    dist_base_dir = settings['dist_base']
    for target in settings['targets']:
        src_dist_dir = join(dist_base_dir, target.get('dist', ''))
        exclude_patterns = target.get('exclude_patterns', [])

        for include_path in get_includes(target):
            # コピー処理
            if isfile(include_path):
                makedirs(src_dist_dir, exist_ok=True)
                copy(include_path, src_dist_dir)
            elif isdir(include_path):
                copytree(
                    include_path,
                    join(src_dist_dir, basename(include_path)),
                    ignore=ignore_patterns(*exclude_patterns))

    print('ビルド成功')


if __name__ == '__main__':
    main()
